using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.User;

namespace StreamingApp.Core.Commands.Chat;
public interface ISendSignalRMessage
{
    Task SendAllertAndEventMessage(User user, MessageDto messageDto, AlertDto alert);
    Task SendChatMessage(User user, MessageDto messageDto);
    Task TranslateMessage(MessageDto messageDto);
}