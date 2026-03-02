using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Interfaces;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Queries.Alerts.Interfaces;
using StreamingApp.Core.Queries.Logic.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class ManageAlert : IManageAlert
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ITwitchSendRequest _twitchSendRequest;

    private readonly ISubAlertLoong _subAlertLoong;

    private readonly IHubContext<ChatHub> _clientHub;

    private readonly IMovingText _movingText;

    private readonly IMessageCheck _messageCheck;

    private readonly IRaidAlert _raidAlert;

    public ManageAlert(UnitOfWorkContext unitOfWork, ITwitchSendRequest twitchSendRequest, ISubAlertLoong subAlertLoong, IHubContext<ChatHub> clientHub, IMovingText movingText, IMessageCheck messageCheck, IRaidAlert raidAlert)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = twitchSendRequest;
        _subAlertLoong = subAlertLoong;
        _clientHub = clientHub;
        _movingText = movingText;
        _messageCheck = messageCheck;
        _raidAlert = raidAlert;
    }

    public async Task<string> ExecuteBitAndRedeamAndFollow(MessageAlertDto messageAlertDto)
    {
        string message = "";

        if (messageAlertDto.AlertType == AlertTypeEnum.Bits)
        {
            // For TTS
            var setting = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == messageAlertDto.Origin);

            if (messageAlertDto.Bits >= setting.TTSAmount && await _messageCheck.ExecuteMessageOnly(messageAlertDto.Message))
            {
                // TODO: Limit Message to a specific lenght
                // TODO: check for "Spam" or same thing in the message and cut it of at a certan time
                // TODO: TTS Message Play
                // TODO: able to skip TTS Message or Mute it => Frontend
                // TODO: Show emote
                MessageDto highlightChatMessage = new(false, messageAlertDto.Channel, messageAlertDto.UserId, messageAlertDto.UserName, messageAlertDto.UserName, messageAlertDto.ColorHex, messageAlertDto.Emotes, new(), OriginEnum.Twitch, new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, messageAlertDto.AlertType.ToString(), null, messageAlertDto.Message, messageAlertDto.EmoteReplacedMessage, DateTime.Now);

                await _clientHub.Clients.All.SendAsync("ReceiveHighlightMessage", highlightChatMessage);
            }

            var bitsAmount = messageAlertDto.Origin == OriginEnum.Twitch ? messageAlertDto.Bits : Convert.ToInt32(Math.Round(messageAlertDto.Currency * setting.Conversion));

            Trigger data = _unitOfWork.Trigger.Include("Targets").Include("Targets.TargetData")
                .Where(t => t.TriggerCondition == Domain.Enums.Trigger.TriggerCondition.Bits && t.Ammount <= bitsAmount && t.Active)
                .Where(t => t.AmmountCloser == false || (t.ExactAmmount == true && t.Ammount == bitsAmount))
                .OrderBy(t => t.Ammount)
                .Last();

            if (data != null)
            {
                //AlertDto alert = t.Alert;

                // TODO: Show emote
                //await _sendSignalRMessage.SendAllertAndEventMessage(user, messageAlertDto, t);
            }

            message = $"Given {bitsAmount} Bits";
        }
        if (messageAlertDto.AlertType == AlertTypeEnum.PointRedeam)
        {
            message = $"Used {messageAlertDto.PointRediam} Point Redeam";

            // TODO: Create Connection with Client APP for VTubeStudio
        }
        if (messageAlertDto.AlertType == AlertTypeEnum.Follow)
        {
            message = $"Fallowed";
        }

        MessageDto chatMessage = new(false, messageAlertDto.Channel, messageAlertDto.UserId, messageAlertDto.UserName, messageAlertDto.UserName, messageAlertDto.ColorHex, messageAlertDto.Emotes, new(), OriginEnum.Twitch, new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, messageAlertDto.AlertType.ToString(), null, message, messageAlertDto.EmoteReplacedMessage, DateTime.Now);
        await _clientHub.Clients.All.SendAsync("displayEventMessages", chatMessage);

        return message;
    }

    public async Task ExecuteRaid(RaidDto raidDto)
    {
        //TODO: add Raider to DB 
        ChannelInfo? channelInfo = await _twitchSendRequest.GetChannelInfo(raidDto.UserName, false);//Fix GetChannelInfo to be used with UserName

        CommandAndResponse? commandAndResponse = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Command.Equals("so") && t.Active);

        if (channelInfo != null)
        {
            string message = commandAndResponse.Response;

            message = message.Replace("[User]", raidDto.UserName);
            message = message.Replace("[GameName]", channelInfo.GameName);
            message = message.Replace("[Url]", $"https://twitch.tv/{raidDto.UserName}");
            //_twitchSendRequest.SendAnnouncement(message);

            string eventMessage = $"{raidDto.utcNow} was last seen Playing {channelInfo.GameName}";

            MessageDto annoucementMessage = new(false, raidDto.UserName, "FFFFFF", null, eventMessage, eventMessage, new(), new(), OriginEnum.Twitch,
                new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, null, "000000", raidDto.UserName, raidDto.UserName, DateTime.Now);

            await _clientHub.Clients.All.SendAsync("ReceiveEventMessage", annoucementMessage);
            //_twitchSendRequest.SendAnnouncement(message);
        }

        string raidMessage = $"raided with {raidDto.Count} Viewers";

        string image = string.Empty;

        //TODO: change that it will use DB for raid Message and image
        switch (raidDto.UserName) {
            case "tiny_karo":
                raidMessage = raidMessage.Replace("Viewers", "Raccoons");
                break;
            case "Myusagii":
                raidMessage = raidMessage.Replace("Viewers", "Mangobuns");
                break;
            case "fufu":
                raidMessage = raidMessage.Replace("Viewers", "Garys");
                break;
        }

        await _clientHub.Clients.All.SendAsync("ReceiveAlert", await _raidAlert.Execute(raidDto.Count, image));

        MessageDto chatMessage = new(false, raidDto.UserName, "FFFFFF", null, raidMessage, raidMessage, new(), new(), OriginEnum.Twitch,
                new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, null, "000000", raidDto.UserName, raidDto.UserName, DateTime.Now);
        
        await _clientHub.Clients.All.SendAsync("ReceiveOnScreenChatMessage", chatMessage);
    }

    public async Task ExecuteSub(SubDto subDto)
    {
        int rotation = new Random().Next(1, 360);
        int saturation = new Random().Next(1, 1000);

        MessageDto chatMessage;
        string message = "";

        if (subDto.IsGifftedSub)
        {
            message = $"{subDto.UserName} Giffted {subDto.GifftedSubCount} {subDto.CurrentTier} Subs";
        }
        else
        {
            message = $"{subDto.UserName} Subscribed with {subDto.CurrentTier} for {subDto.SubLenght} Months";
        }

        if (subDto.ChatMessage != null)
        {
            chatMessage = new(false, subDto.Channel, subDto.ChatMessage.ColorHex, null, $"{message}", subDto.ChatMessage.Message, subDto.ChatMessage.Emotes, new(), OriginEnum.Twitch,
                new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, subDto.MessageId, subDto.UserId, subDto.UserName, subDto.UserName, DateTime.Now);
        }
        else
        {
            chatMessage = new(false, subDto.Channel, subDto.ChatMessage.ColorHex, null, message, message, null, new(), OriginEnum.Twitch,
                new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, subDto.MessageId, subDto.UserId, subDto.UserName, subDto.UserName, DateTime.Now);
        }

        //await _subAlertLoong.Execute(subDto.DisplayName, subDto.GifftedSubCount, rotation, saturation, true, true);

        Console.WriteLine("SubMessage ",message);
        await _clientHub.Clients.All.SendAsync("ReceiveEventMessage", chatMessage);

        await _movingText.ExecuteAlert(30, message);
    }
}
