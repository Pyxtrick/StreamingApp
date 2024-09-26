using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IGameCommand
{
    void Execute(CommandAndResponse commandAndResponse);
}