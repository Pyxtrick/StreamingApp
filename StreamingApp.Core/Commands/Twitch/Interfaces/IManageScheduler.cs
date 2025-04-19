using StreamingApp.Domain.Entities.InternalDB.Trigger;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;
public interface IManageScheduler
{
    Task Execute(Trigger trigger);
}