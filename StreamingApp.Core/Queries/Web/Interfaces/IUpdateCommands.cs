using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface IUpdateCommands
{
    List<CommandAndResponseDto> UpdtateAll(List<CommandAndResponseDto> commands);
}