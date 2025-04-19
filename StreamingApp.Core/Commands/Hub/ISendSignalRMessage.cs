using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.User;

namespace StreamingApp.Core.Commands.Hub;
public interface ISendSignalRMessage
{
    Task SendAlertAndEventMessage(User user, MessageDto messageDto, AlertDto alert);
    Task SendChatMessage(User user, MessageDto messageDto);
    Task SendBannedEventMessage(BannedUserDto bannedUser);
    Task TranslateMessage(MessageDto messageDto);
}