using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IManageMessages
{
    Task Execute();

    Task ExecuteMultiple(List<MessageDto> messages);

    Task ExecuteOne(MessageDto messages);
}