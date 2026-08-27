using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IManageStreamCommands
{
    Task Execute(MessageDto messageDto);
}