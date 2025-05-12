using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries.Alerts;
public interface ISubAlertLoong
{
    Task<AlertDto> Execute(string userName, int Length, int rotation, int saturation, bool directionltr);
}