using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IManageStream
{
    Task Execute(MessageDto messageDto);
    Task ChangeCategory();
    Task StartOrEndStream();
}