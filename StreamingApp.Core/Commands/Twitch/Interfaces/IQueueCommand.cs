using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IQueueCommand
{
    void Execute(CommandAndResponse commandAndResponse, string message, string userName, ChatOriginEnum origin);
}