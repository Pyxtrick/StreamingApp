using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Interfaces;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Queries.Alerts;
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

    public ManageAlert(UnitOfWorkContext unitOfWork, ITwitchSendRequest twitchSendRequest, ISubAlertLoong subAlertLoong, IHubContext<ChatHub> clientHub)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = twitchSendRequest;
        _subAlertLoong = subAlertLoong;
        _clientHub = clientHub;
    }

    public async Task ExecuteBitAndRedeamAndFollow(MessageAlertDto messageAlertDto)
    {
        if (messageAlertDto.AlertType == AlertTypeEnum.Bits)
        {
            if (messageAlertDto.Bits >= 200)
            {
                // TODO: Limit Message to a specific lenght
                // TODO: check for "Spam" or same thing in the message and cut it of at a certan time
                // TODO: TTS Message Play
                // TODO: able to skip TTS Message or Mute it
                // TODO: Show emote
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
        }
        if (messageAlertDto.AlertType == AlertTypeEnum.PointRedeam)
        {
            // TODO: Create Connection with Client APP for VTubeStudio
        }
        if (messageAlertDto.AlertType == AlertTypeEnum.Follow)
        {

        }
    }

    public async Task ExecuteRaid(RaidDto raidDto)
    {
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

            MessageDto chatMessage = new(raidDto.UserName, false, "local", "userid", "", "", "#ff6b6b", "", $"{raidDto.UserName} raided with {raidDto.Count} Viewers", null, new(), new(), ChatOriginEnum.Twitch, new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, DateTime.Now);
            await _clientHub.Clients.All.SendAsync("ReceiveHighlightMessage", chatMessage);
        }
    }

    public async Task ExecuteSub(SubDto subDto)
    {
        int rotation = new Random().Next(1, 360);
        int saturation = new Random().Next(1, 1000);

        await _subAlertLoong.Execute(subDto.DisplayName, subDto.GifftedSubCount, rotation, saturation, true);
    }
}
