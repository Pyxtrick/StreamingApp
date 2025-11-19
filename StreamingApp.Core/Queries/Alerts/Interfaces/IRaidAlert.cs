using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries.Alerts.Interfaces;
public interface IRaidAlert
{
    Task<AlertDto> Execute(int count, string? image);
}