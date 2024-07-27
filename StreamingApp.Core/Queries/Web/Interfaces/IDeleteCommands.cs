using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface IDeleteCommands
{
    bool Delete(List<CommandAndResponseDto> commands);
}