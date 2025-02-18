namespace StreamingApp.Core.Commands.DB.Interfaces;

public interface IUpdateUserAchievementsOnDB
{
    Task UpdateAchievements(int userId);
}