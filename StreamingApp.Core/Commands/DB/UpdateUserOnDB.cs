using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.Interfaces;
using StreamingApp.DB;
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
    public async Task UpdateAchievements(int userId)
    {
        var stream = await _unitOfWork.StreamHistory.OrderBy(sh => sh.StreamStart).LastAsync();

        if (stream.StreamEnd == stream.StreamStart)
        {
            User user = _unitOfWork.User.Where(u => u.Id == userId).Include("TwitchAchievements").ToList().First();

            var achievements = user.TwitchAchievements;

            if (achievements.LastStreamSeen < stream.StreamStart)
            {
                achievements.LastStreamSeen = DateTime.UtcNow;
                achievements.WachedStreams++;

                //_unitOfWork.Achievements.Add(achievements);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateAuth(int userId, List<AuthEnum> auths)
    {
        User user = _unitOfWork.User.Where(u => u.Id == userId).Include("Status").ToList().First();
        if (user != null)
        {
            user.Status.IsVIP = auths.Contains(AuthEnum.Vip);
            user.Status.IsVerified = auths.Contains(AuthEnum.Partner);
            user.Status.IsMod = auths.Contains(AuthEnum.Mod);
            user.Status.IsRaider = auths.Contains(AuthEnum.Raider);

            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task UpdateSub(int userId, bool isSub, TierEnum tier, int subTime)
    {
        User user = _unitOfWork.User.Where(u => u.Id == userId).Include("Status").Include("TwitchSub").ToList().First();
        if (user != null)
        {
            user.Status.TwitchSub.CurrentySubscribed = isSub;
            user.Status.TwitchSub.SubscribedTime = subTime;
            user.Status.TwitchSub.CurrentTier = tier;
            
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
