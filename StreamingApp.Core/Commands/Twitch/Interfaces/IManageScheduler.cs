using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;
public interface IManageScheduler
{
    Task Execute(Trigger trigger);
}