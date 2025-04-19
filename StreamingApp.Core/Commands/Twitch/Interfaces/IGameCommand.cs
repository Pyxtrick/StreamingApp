using StreamingApp.Domain.Entities.InternalDB.Trigger;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IGameCommand
{
    void Execute(CommandAndResponse commandAndResponse);
}