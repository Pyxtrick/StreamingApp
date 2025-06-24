using AutoMapper;
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
        List<User> users = _unitOfWork.User.Include("Details").Include("Status").Include("Achievements").ToList();

        return users.Select(_mapper.Map<UserDto>).ToList();
    }

    public async Task<User> CreateOne(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth, OriginEnum Origin)
    {
        User user = _unitOfWork.User.Include("Details").Where(t => t.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).ExternalUserId == twitchUserId).ToList().FirstOrDefault();

        if (user != null)
        {
            User userTwo = _unitOfWork.User.Where(t => t.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).UserName == userName).ToList().FirstOrDefault();

            // Check for userName change
            if (userTwo == null)
            {
                user.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).UserName = userName;

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
                Details = new()
                {
                    new()
                    {
                        ExternalUserId = twitchUserId,
                        UserName = userName,
                        AppAuthEnum = AppAuthEnum.user
                    }
                },
                Achievements = new()
                {
                    new()
                    {
                        FirstStreamSeen = DateTime.Now,
                        LastStreamSeen = DateTime.Now,
                        GiftedSubsCount = 0,
                        GiftedBitsCount = 0,
                        GiftedDonationCount = 0,
                        WachedStreams = 1
                    }
                },
                Status = new()
                {
                    Subs = new()
                    {
                        new()
                        {
                            CurrentySubscribed = Origin == OriginEnum.Twitch ? isSub : false,
                            SubscribedTime = Origin == OriginEnum.Twitch ? subTime : 0,
                            CurrentTier = Origin == OriginEnum.Twitch ? (isSub ? TierEnum.Tier1 : TierEnum.None) : TierEnum.None, // cannot get data ATM
                        }
                        /**
                        new()
                        {
                            CurrentySubscribed = Origin == OriginEnum.Youtube ? isSub : false,
                            SubscribedTime = Origin == OriginEnum.Youtube ? subTime : 0,
                            CurrentTier = Origin == OriginEnum.Youtube ? (isSub ? TierEnum.Tier1 : TierEnum.None) : TierEnum.None, // cannot get data ATM
                        }*/
                    },
                    FirstChatDate = DateTime.Now,
                    FallowDate = DateTime.Now, // cannot get data ATM
                    IsVIP = auth.Contains(AuthEnum.Vip),
                    IsVerified = auth.Contains(AuthEnum.Partner),
                    IsMod = auth.Contains(AuthEnum.Mod),
                    AutoShoutout = false,
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

    /// <summary>
    /// Combine User from Twitch and YouTube
    /// </summary>
    /// <returns>True if Firts Message of Stream</returns>
    public async Task CombineUser(string twitchUserId, string youtubeUserId)
    {
        User user = _unitOfWork.User.Include("Achievements").Include("Details").Where(u => u.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).ExternalUserId == twitchUserId).ToList().First();
        
        // TODO: Combine both user (add data from the newest one to the oldest one + remove the one that is not needed anymore)
    }

    public async Task<bool> UpdateAchievements(string userId, OriginEnum Origin)
    {
        var stream = await _unitOfWork.StreamHistory.OrderBy(sh => sh.StreamStart).LastAsync();

        if (stream.StreamEnd == stream.StreamStart)
        {
            User user = null;

            if (Origin == OriginEnum.Twitch)
            {
                user = _unitOfWork.User.Include("Achievements").Include("Details").FirstOrDefault(u => u.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).ExternalUserId == userId);
            }
            else if(Origin == OriginEnum.Youtube)
            {
                //user = _unitOfWork.User.Include("YouTubeAchievements").Include("YouTubeDetail").Where(u => u.Youtube.UserId == userId).ToList().First();
            }

            if(user == null)
            {
                return false;
            }

            var achievements = user.Achievements.FirstOrDefault(t => t.Origin == OriginEnum.Twitch);

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

    public async Task<bool> UpdateAuth(string userId, List<AuthEnum> auths, OriginEnum Origin)
    {
        User user = _unitOfWork.User.Include("Status").Include("Details").Where(u => u.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).ExternalUserId == userId).ToList().First();
        if (user != null)
        {
            user.Status.IsVIP = auths.Contains(AuthEnum.Vip);
            user.Status.IsVerified = auths.Contains(AuthEnum.Partner);
            user.Status.IsMod = auths.Contains(AuthEnum.Mod);

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

    public async Task<bool> UpdateSub(string userId, bool isSub, TierEnum tier, int subTime, OriginEnum Origin)
    {
        User user = _unitOfWork.User.Include("Status").Include("Details").Where(u => u.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).ExternalUserId == userId).ToList().First();
        if (user != null)
        {
            var twitchSub = user.Status.Subs.FirstOrDefault(t => t.Origin == OriginEnum.Twitch);

            if (isSub)
            {
                twitchSub.CurrentySubscribed = isSub;
                twitchSub.SubscribedTime = twitchSub.SubscribedTime <= subTime ? subTime : twitchSub.SubscribedTime++;
                twitchSub.CurrentTier = tier;
            }
            else if (twitchSub.CurrentySubscribed == true)
            {
                twitchSub.CurrentySubscribed = isSub;
                twitchSub.CurrentTier = tier;
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

    public async Task<bool> UpdateBan(string userId, BannedUserDto bannedUserDto, OriginEnum Origin)
    {
        User user = _unitOfWork.User.Include("Ban").Include("Details").Where(u => u.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).ExternalUserId == userId).ToList().First();

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

    public async Task<bool> Delete(List<UserDto> users, OriginEnum Origin)
    {
        try
        {
            foreach (UserDto user in users)
            {
                var removeData = _unitOfWork.User.Include("Details").Include("Status").Include("Achievements").Include("Ban").FirstOrDefault(t => t.Id == user.Id);

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
