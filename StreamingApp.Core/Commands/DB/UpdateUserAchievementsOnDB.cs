using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal.User;

namespace StreamingApp.Core.Commands.DB;

public class UpdateUserAchievementsOnDB : IUpdateUserAchievementsOnDB
{
    private readonly UnitOfWorkContext _unitOfWork;

    public UpdateUserAchievementsOnDB(UnitOfWorkContext unitOfWork)
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
            User data = _unitOfWork.User.Where(u => u.Id == userId).Include("TwitchAchievements").ToList().First();

            Achievements? achievements = data.TwitchAchievements;

            if (achievements.LastStreamSeen < stream.StreamStart)
            {
                achievements.LastStreamSeen = DateTime.UtcNow;
                achievements.WachedStreams++;

                //_unitOfWork.Achievements.Add(achievements);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
