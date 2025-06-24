using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;
public interface IManageAchievements
{
    Task ExecuteBit(MessageAlertDto sub);
    Task ExecuteSub(SubDto sub);
}