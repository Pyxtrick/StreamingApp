using Microsoft.EntityFrameworkCore;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Commands.DB;

public class UpdateUserAchievementsOnDB : IUpdateUserAchievementsOnDB
{
    private readonly UnitOfWorkContext _unitOfWork;

    public UpdateUserAchievementsOnDB(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateAchievements(int userId)
    {
        User data = _unitOfWork.User.Where(u => u.Id == userId).Include("TwitchAchievements").ToList().First();

        Achievements? achievements = data.TwitchAchievements;

        achievements.LastStreamSeen = DateTime.Now;
        achievements.WachedStreams++;

        _unitOfWork.Achievements.Add(achievements);
        await _unitOfWork.SaveChangesAsync();
    }
}
