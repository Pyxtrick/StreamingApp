﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.CRUD;

public class CRUDUsers : ICRUDUsers
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDUsers(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAll()
    {
        List<User> users = _unitOfWork.User.Include("TwitchDetail").Include("Status").Include("TwitchAchievements").ToList();

        return users.Select(_mapper.Map<UserDto>).ToList();
    }

    public async Task<User> CreateOne(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth)
    {
        User user = _unitOfWork.User.Include("TwitchDetail").Where(t => t.TwitchDetail.UserId == twitchUserId).ToList().FirstOrDefault();

        if (user != null)
        {
            User userTwo = _unitOfWork.User.Where(t => t.TwitchDetail.UserName == userName).ToList().FirstOrDefault();

            // Check for userName change
            if (userTwo == null)
            {
                user.TwitchDetail.UserName = userName;

                _unitOfWork.User.Update(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        if (user == null)
        {
            // TODO: make a Class for this in API.Twitch
            //GetHypeTrainResponse hypeTrain = await _twitchCache.GetTheTwitchAPI().Helix.HypeTrain.GetHypeTrainEventsAsync(_configuration["Twitch:ClientId"], 1, null);

            User newUser = new User()
            {
                UserText = userName,
                TwitchDetail = new()
                {
                    UserId = twitchUserId,
                    UserName = userName,
                    AppAuthEnum = AppAuthEnum.user
                },
                TwitchAchievements = new()
                {
                    FirstStreamSeen = DateTime.Now,
                    LastStreamSeen = DateTime.Now,
                    GiftedSubsCount = 0,
                    GiftedBitsCount = 0,
                    GiftedDonationCount = 0,
                    WachedStreams = 1
                },
                Status = new()
                {
                    TwitchSub = new()
                    {
                        CurrentySubscribed = isSub,
                        SubscribedTime = subTime,
                        CurrentTier = isSub ? TierEnum.Tier1 : TierEnum.None, // cannot get data ATM
                    },
                    FirstChatDate = DateTime.Now,
                    FallowDate = DateTime.Now, // cannot get data ATM
                    IsVIP = auth.Contains(AuthEnum.Vip),
                    IsVerified = auth.Contains(AuthEnum.Partner),
                    IsMod = auth.Contains(AuthEnum.Mod),
                    IsRaider = auth.Contains(AuthEnum.Raider),
                    TimeZone = "waiting",
                    UserType = UserTypeEnum.Viewer,
                },
                Ban = new()
                {
                    IsBaned = false,
                    IsExcludeQueue = false,
                    ExcludePole = false,
                    IsExcludeChat = false,
                    ExcludeReason = "",
                    TimeOutAmount = 0,
                    MessagesDeletedAmount = 0,
                    BanedAmount = 0,
                    BanedDate = null,
                    LastMessage = ""
                }
            };

            _unitOfWork.User.Add(newUser);
            await _unitOfWork.SaveChangesAsync();

            return newUser;
        }

        return user;
    }

    public async Task<bool> UpdateAchievements(string userId)
    {
        var stream = await _unitOfWork.StreamHistory.OrderBy(sh => sh.StreamStart).LastAsync();

        if (stream.StreamEnd == stream.StreamStart)
        {
            User user = _unitOfWork.User.Include("TwitchAchievements").Include("TwitchDetail").Where(u => u.TwitchDetail.UserId == userId).ToList().First();

            var achievements = user.TwitchAchievements;

            if (achievements.LastStreamSeen < stream.StreamStart)
            {
                achievements.LastStreamSeen = DateTime.UtcNow;
                achievements.WachedStreams++;

                //_unitOfWork.Achievements.Update(achievements);
                try
                {
                    await _unitOfWork.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        
        return false;
    }

    public async Task<bool> UpdateAuth(string userId, List<AuthEnum> auths)
    {
        User user = _unitOfWork.User.Include("Status").Include("TwitchDetail").Where(u => u.TwitchDetail.UserId == userId).ToList().First();
        if (user != null)
        {
            user.Status.IsVIP = auths.Contains(AuthEnum.Vip);
            user.Status.IsVerified = auths.Contains(AuthEnum.Partner);
            user.Status.IsMod = auths.Contains(AuthEnum.Mod);
            user.Status.IsRaider = auths.Contains(AuthEnum.Raider);

            try
            {
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        return false;
    }

    public async Task<bool> UpdateSub(string userId, bool isSub, TierEnum tier, int subTime)
    {
        User user = _unitOfWork.User.Include("Status").Include("TwitchDetail").Where(u => u.TwitchDetail.UserId == userId).ToList().First();
        if (user != null)
        {
            if (isSub)
            {
                user.Status.TwitchSub.CurrentySubscribed = isSub;
                user.Status.TwitchSub.SubscribedTime = user.Status.TwitchSub.SubscribedTime <= subTime ? subTime : user.Status.TwitchSub.SubscribedTime++;
                user.Status.TwitchSub.CurrentTier = tier;
            }
            else if (user.Status.TwitchSub.CurrentySubscribed == true)
            {
                user.Status.TwitchSub.CurrentySubscribed = isSub;
                user.Status.TwitchSub.CurrentTier = tier;
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        return false;
    }

    public async Task<bool> UpdateBan(string userId, BannedUserDto bannedUserDto)
    {
        User user = _unitOfWork.User.Include("Ban").Include("TwitchDetail").Where(u => u.TwitchDetail.UserId == userId).ToList().First();

        if (bannedUserDto.TargetEnum == BannedTargetEnum.Message)
        {
            user.Ban.MessagesDeletedAmount++;
        }
        else if (bannedUserDto.TargetEnum == BannedTargetEnum.Message || bannedUserDto.TargetEnum == BannedTargetEnum.Message)
        {
            user.Ban.BanedDate = bannedUserDto.Date;
            user.Ban.LastMessage = bannedUserDto.LastMessage;
            user.Ban.IsWatchList = true;
            user.Ban.MessagesDeletedAmount++;

            if (bannedUserDto.TargetEnum == BannedTargetEnum.Banned)
            {
                user.Ban.BanedAmount++;
                user.Ban.IsBaned = true;
            }
            else if (bannedUserDto.TargetEnum == BannedTargetEnum.TimeOut)
            {
                user.Ban.TimeOutAmount++;
            }
        }

        try
        {
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> Delete(List<UserDto> users)
    {
        try
        {
            foreach (UserDto user in users)
            {
                var removeData = _unitOfWork.User.Include("TwitchDetail").Include("Status").Include("TwitchAchievements").Include("Ban").FirstOrDefault(t => t.Id == user.Id);

                if (removeData != null)
                {
                    _unitOfWork.Remove(removeData);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
