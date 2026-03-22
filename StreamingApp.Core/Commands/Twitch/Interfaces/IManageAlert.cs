using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IManageAlert
{
    Task<string> ExecuteBitAndRedeamAndFollow(MessageAlertDto messageAlertDto);

    Task ExecuteRaid(RaidDto raidDto);

    Task ExecuteSub(SubDto subDto);
}