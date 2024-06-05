namespace StreamingApp.Core.Commands.DB;

public interface IUpdateUserAchievementsOnDB
{
    Task UpdateAchievements(int userId);
}