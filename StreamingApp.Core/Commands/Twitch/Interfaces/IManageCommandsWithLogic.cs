using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.Trigger;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IManageCommandsWithLogic
{
    Task Execute(MessageDto commandDto, CommandAndResponse commandAndResponse);
}