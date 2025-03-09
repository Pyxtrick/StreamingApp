using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;
internal class ManageAlert : IManageAlert
{
    private readonly UnitOfWorkContext _unitOfWork;

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
    }

    public async Task ExecuteSub(SubDto subDto)
    {
        Console.WriteLine(subDto.UserName);
    }
}
