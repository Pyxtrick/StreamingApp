using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IGameCommand
{
    void Execute(CommandAndResponse commandAndResponse);
}