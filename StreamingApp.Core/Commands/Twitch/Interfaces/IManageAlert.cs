using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;
internal interface IManageAlert
{
    Task ExecuteBitAndRedeamAndFollow(MessageAlertDto messageAlertDto);

    Task ExecuteRaid(RaidDto raidDto);

    Task ExecuteSub(SubDto subDto);
}