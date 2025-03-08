using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB;

public class UpdateUserOnDB : IUpdateUserOnDB
{
    private readonly UnitOfWorkContext _unitOfWork;

    public UpdateUserOnDB(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Add WatchStream streak if stream is active / Live
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task UpdateAchievements(string userId)
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
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateAuth(string userId, List<AuthEnum> auths)
    {
        User user = _unitOfWork.User.Include("Status").Include("TwitchDetail").Where(u => u.TwitchDetail.UserId == userId).ToList().First();
        if (user != null)
        {
            user.Status.IsVIP = auths.Contains(AuthEnum.Vip);
            user.Status.IsVerified = auths.Contains(AuthEnum.Partner);
            user.Status.IsMod = auths.Contains(AuthEnum.Mod);
            user.Status.IsRaider = auths.Contains(AuthEnum.Raider);

            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task UpdateSub(string userId, bool isSub, TierEnum tier, int subTime)
    {
        User user = _unitOfWork.User.Include("Status").Include("TwitchDetail").Where(u => u.TwitchDetail.UserId == userId).ToList().First();
        if (user != null)
        {
            if (isSub)
            {
                user.Status.TwitchSub.CurrentySubscribed = isSub;
                user.Status.TwitchSub.SubscribedTime = user.Status.TwitchSub.SubscribedTime <= subTime ? subTime : user.Status.TwitchSub.SubscribedTime++;
                user.Status.TwitchSub.CurrentTier = tier;

                await _unitOfWork.SaveChangesAsync();
            }
            else if(user.Status.TwitchSub.CurrentySubscribed == true)
            {
                user.Status.TwitchSub.CurrentySubscribed = isSub;
                user.Status.TwitchSub.CurrentTier = tier;
            }
        }
    }

    public async Task UpdateBan(string userId, BannedUserDto bannedUserDto)
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

        await _unitOfWork.SaveChangesAsync();
    }
}
