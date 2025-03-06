namespace StreamingApp.Core.Queries.Achievements;

public interface ICreateFinalStreamAchievements
{
    Task<string> Execute();
}