using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Queries;
public interface IGetTwitchChatData
{
    List<ChatDto> Execute();
}