﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Queries.Translate;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;
using System.Text.RegularExpressions;

namespace StreamingApp.Core.Commands.Hub;

public class SendSignalRMessage : ISendSignalRMessage
{
    private readonly ITranslate _translate;

    private readonly IHubContext<ChatHub> _hubContext;

    private readonly IConfiguration _configuration;

    private readonly UnitOfWorkContext _unitOfWork;

    public SendSignalRMessage(IHubContext<ChatHub> hubContext, ITranslate translate, IConfiguration configuration, UnitOfWorkContext unitOfWorkContext)
    {
        _translate = translate;
        _hubContext = hubContext;
        _configuration = configuration;
        _unitOfWork = unitOfWorkContext;
    }

    // ReceiveSpecialMessage
    // ReceiveWatchUserMessage

    // ReceiveChatMessage

    // ReceiveVipViewerMessage
    // ReceiveModMessage
    // ReceiveFriendMessage
    // ReceiveBotMessage

    // ReceiveModActionMessage

    // ReceiveWatchUserMessage

    // ReceiveAllert
    // ReceiveEventMessage

    // ReceiveTranslatedMessage

    // ReceiveYoutubeMessage

    /// <summary>
    /// Uses SignalR to send the message to the correct chat in the Frontend
    /// </summary>
    /// <param name="user"></param>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    public async Task SendChatMessage(User user, MessageDto messageDto)
    {
        /** TODO Implement Translate
        if (_translate.GetLanguage(messageDto.Message) != "eng" && _translate.GetLanguage(messageDto.Message) != "de")
        {
            await TranslateMessage(messageDto);
        }**/

        // Display chat with out Bots
        if (user.Status.UserType != UserTypeEnum.Bot || user.Ban.IsExcludeChat == false)
        {
            // TODO: do someting about when Stream Together is Active
            await _hubContext.Clients.All.SendAsync("ReceiveChatMessage", messageDto);

            // DO not allow to show links of any kink on the OnScreenChat
            if (new Regex("([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?", RegexOptions.IgnoreCase).Match(messageDto.Message).Success == false
                && messageDto.Channel.Equals(_configuration["Twitch:Channel"], StringComparison.InvariantCultureIgnoreCase))
            {
                var settings = _unitOfWork.Settings.First(s => s.Origin == messageDto.Origin);

                // Limit OnScreenChatters using settings.AllChat without limiting the Twitch Chat
                if (settings != null && messageDto.Auth.Min() <= settings.AllChat)
                {
                    // TODO: Change Send Location when messageDto.Origin is not Twitch
                    await _hubContext.Clients.All.SendAsync("ReceiveOnScreenChatMessage", messageDto);
                }
            }
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
        if (user.Ban.IsWatchList && user.Ban.IsExcludeChat && user.Ban.IsExcludeQueue && user.Ban.IsBaned)
        {
            string messageAdon = "";
            if (user.Ban.IsWatchList)
            {
                messageAdon += " WatchList ";
            }
            if (user.Ban.IsExcludeChat)
            {
                messageAdon += " ExcludeChat ";
            }
            if (user.Ban.IsExcludeQueue)
            {
                messageAdon += " ExcludeQueue ";
            }
            if (user.Ban.IsBaned)
            {
                messageAdon += " Banned ";
            }
            messageDto.ReplayMessage = messageAdon;

            await _hubContext.Clients.All.SendAsync("ReceiveWatchUserMessage", messageDto);
        }
    }

    // TODO: to Be Implemented Send Allert and Event Message
    public async Task SendAlertAndEventMessage(User user, MessageDto messageDto, AlertDto alert)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveAlert", alert);
        await _hubContext.Clients.All.SendAsync("ReceiveEventMessage", messageDto);
    }

    public async Task SendBannedEventMessage(BannedUserDto bannedUser)
    {
        // This can differ for Banned, TimeOut and Deleted Message
        await _hubContext.Clients.All.SendAsync("ReceiveBanned", bannedUser);
    }

    /// <summary>
    /// TODO: to Be Implemented Translation to English
    /// </summary>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    public async Task TranslateMessage(MessageDto messageDto)
    {
        messageDto.Message = await _translate.TranslateMessage(messageDto.Message);

        await _hubContext.Clients.All.SendAsync("ReceiveTranslatedMessage", messageDto);
    }
}
