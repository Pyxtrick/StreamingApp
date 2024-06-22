using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface IGetCommands
{
    List<CommandAndResponseDto> GetAll();
}