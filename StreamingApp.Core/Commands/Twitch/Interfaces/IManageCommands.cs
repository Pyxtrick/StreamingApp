using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;
public interface IManageCommands
{
    Task Execute(MessageDto messageDto);
}