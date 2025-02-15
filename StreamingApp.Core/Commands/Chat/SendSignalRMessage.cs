using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StreamingApp.API.SignalRHub;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Chat;

public class SendSignalRMessage : ISendSignalRMessage
{
    private readonly ITranslate _translate;

    private readonly IHubContext<ChatHub> _hubContext;

    public SendSignalRMessage(IHubContext<ChatHub> hubContext, ITranslate translate)
    {
        _translate = translate;
        _hubContext = hubContext;
    }

    // ReceiveChatMessage

    // ReceiveVipViewerMessage
    // ReceiveModMessage
    // ReceiveFriendMessage
    // ReceiveBotMessage

    // ReceiveWatchUserMessage

    // ReceiveAllert
    // ReceiveEventMessage

    // ReceiveTranslateMessage

    /// <summary>
    /// Uses SignalR to send the message to the correct chat in the Frontend
    /// </summary>
    /// <param name="user"></param>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    public async Task SendChatMessage(User user, MessageDto messageDto)
    {
        if (_translate.GetLanguage(messageDto.Message) != "eng" && _translate.GetLanguage(messageDto.Message) != "de")
        {
            await TranslateMessage(messageDto);
        }

        // Display chat with out Bots
        if (user.Status.UserType != UserTypeEnum.Bot || user.Ban.IsExcludeChat)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveChatMessage", messageDto);
        }

        // Chat specific UserType
        if (user.Status.UserType == UserTypeEnum.VipViewer
            || user.Status.UserType != UserTypeEnum.Mod
            || user.Status.UserType != UserTypeEnum.Friend
            || user.Status.UserType != UserTypeEnum.Bot)
        {
            string UserType = user.Status.UserType.ToString();
            string Target = $"Receive{UserType}Message";

            await _hubContext.Clients.All.SendAsync(Target, messageDto);
        }

        // Special message for firstMessage 
        if (messageDto.SpecialMessage.Contains(SpecialMessgeEnum.FirstMessage) ||
            messageDto.SpecialMessage.Contains(SpecialMessgeEnum.FirstStreamMessage) ||
            messageDto.SpecialMessage.Contains(SpecialMessgeEnum.Highlighted))
        {
            await _hubContext.Clients.All.SendAsync("ReceiveSpecialMessage", messageDto);
        }

        // Watch List Users
        if (user.Ban.IsExcludeChat)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveWatchUserMessage", messageDto);
        }
    }

    // TODO: to Be Implemented Send Allert and Event Message
    public async Task SendAllertAndEventMessage(User user, MessageDto messageDto, Trigger trigger)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveAllert");
        await _hubContext.Clients.All.SendAsync("ReceiveEventMessage", messageDto);
    }

    /// <summary>
    /// TODO: to Be Implemented Translation to English
    /// </summary>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    public async Task TranslateMessage(MessageDto messageDto)
    {
        messageDto.Message = await _translate.TranslateMessage(messageDto.Message);

        await _hubContext.Clients.All.SendAsync("ReceiveTranslateMessage", messageDto);
    }
}
