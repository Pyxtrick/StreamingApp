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

internal class ManageAlert : IManageAlert
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

    public async Task ExecuteBitAndRedeamAndFollow(MessageAlertDto messageAlertDto)
    {
        string message = "";

        if (messageAlertDto.AlertType == AlertTypeEnum.Bits)
        {
            if (messageAlertDto.Bits >= 100)
            {
                // TODO: Limit Message to a specific lenght
                // TODO: check for "Spam" or same thing in the message and cut it of at a certan time
                // TODO: TTS Message Play
                // TODO: able to skip TTS Message or Mute it
                // TODO: Show emote
                if (await _messageCheck.ExecuteMessageOnly(messageAlertDto.Message))
                {
                    MessageDto highlightChatMessage = new(false, messageAlertDto.Channel, messageAlertDto.UserId, messageAlertDto.UserName, messageAlertDto.UserName, messageAlertDto.ColorHex, messageAlertDto.Emotes, new(), OriginEnum.Twitch, new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, messageAlertDto.AlertType.ToString(), null, messageAlertDto.Message, messageAlertDto.EmoteReplacedMessage, DateTime.Now);

                    await _clientHub.Clients.All.SendAsync("ReceiveHighlightMessage", highlightChatMessage);
                }
            }

            List<Trigger> data = _unitOfWork.Trigger.Include("Targets").Include("TargetData").ToList();

            int found = 0;

            for (int i = 0; i < data.Count; i++)
            {
                data = ((List<Trigger>)data.Where(t => t.TriggerCondition == Domain.Enums.Trigger.TriggerCondition.Bits)).OrderBy(t => t.Ammount).ToList();

                if (messageAlertDto.Bits == data[i].Ammount)
                {
                    var t = data[i].Targets.First(t => t.TargetCondition == Domain.Enums.Trigger.TargetCondition.Allert).TargetData;

                    //AlertDto alert = t.Alert;

                    // TODO: Show emote
                    //await _sendSignalRMessage.SendAllertAndEventMessage(user, messageAlertDto, t);
                }
                else
                {
                    if (messageAlertDto.Bits < data[i].Ammount)
                    {
                        if (data[i].Active && data[i].ExactAmmount == false)
                        {
                            found = i;
                        }
                    }
                    else if (messageAlertDto.Bits > data[i].Ammount)
                    {
                        // TODO: Show emote
                    }
                }
            }

            message = $"Given {messageAlertDto.Bits} Bits";
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
    }

    public async Task ExecuteRaid(RaidDto raidDto)
    {
        //TODO: add Raider to DB 
        //await _subAlertLoong.Execute(raidDto.UserName, raidDto.Count, 0, 0, true, false);

        Console.WriteLine(raidDto.UserName);

        ChannelInfo? channelInfo = await _twitchSendRequest.GetChannelInfo(raidDto.UserName, false);//Fix GetChannelInfo to be used with UserName

        CommandAndResponse? commandAndResponse = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Command.Equals("so") && t.Active);

        if (channelInfo != null)
        {
            string message = commandAndResponse.Response;

            message = message.Replace("[User]", raidDto.UserName);
            message = message.Replace("[GameName]", channelInfo.GameName);
            message = message.Replace("[Url]", $"https://twitch.tv/{raidDto.UserName}");
            _twitchSendRequest.SendAnnouncement(message);

            Console.WriteLine($"Raid {message}");
        }

        string raidMessage = $"{raidDto.UserName} raided with {raidDto.Count} Viewers";

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

        Console.WriteLine(raidMessage);

        await _clientHub.Clients.All.SendAsync("ReceiveAlert", await _raidAlert.Execute(raidDto.Count, image));

        /**MessageDto chatMessage = new("raidId", false, "local", "userid", "", "", "#ff6b6b", "", raidMessage, null, new(), new(), OriginEnum.Twitch, new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, DateTime.Now);
        await _clientHub.Clients.All.SendAsync("ReceiveHighlightMessage", chatMessage);**/
        await _movingText.ExecuteAlert(30, raidMessage);
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
            message = $"{subDto.UserName} Subscribed with Tier {subDto.CurrentTier} for {subDto.SubLenght}";
        }

        if (subDto.ChatMessage != null)
        {
            chatMessage = new(false, subDto.Channel, subDto.ChatMessage.ColorHex, null, $"{message} |||| {subDto.ChatMessage.Message}", subDto.ChatMessage.EmoteReplacedMessage, subDto.ChatMessage.Emotes, new(), OriginEnum.Twitch,
                new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, subDto.MessageId, subDto.UserId, subDto.UserName, subDto.UserName, DateTime.Now);
        }
        else
        {
            chatMessage = new(false, subDto.Channel, subDto.ChatMessage.ColorHex, null, message, message, null, new(), OriginEnum.Twitch,
                new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, subDto.MessageId, subDto.UserId, subDto.UserName, subDto.UserName, DateTime.Now);
        }

        //await _subAlertLoong.Execute(subDto.DisplayName, subDto.GifftedSubCount, rotation, saturation, true, true);

        await _clientHub.Clients.All.SendAsync("ReceiveEventMessage", chatMessage);

        await _movingText.ExecuteAlert(30, message);
    }
}
