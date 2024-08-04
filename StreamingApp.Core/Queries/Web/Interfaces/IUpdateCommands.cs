using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface IUpdateCommands
{
    List<CommandAndResponseDto> CreateOrUpdtateAll(List<CommandAndResponseDto> commands);
}