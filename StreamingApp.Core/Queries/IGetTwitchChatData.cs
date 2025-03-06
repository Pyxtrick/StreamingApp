using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries;
public interface IGetTwitchChatData
{
    List<MessageDto> Execute();
}