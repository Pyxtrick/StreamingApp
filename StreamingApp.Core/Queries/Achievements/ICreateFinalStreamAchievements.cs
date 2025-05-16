using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries.Achievements;

public interface ICreateFinalStreamAchievements
{
    Task<AlertDto> Execute();
}