using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDCommands
{
    List<CommandAndResponseDto> GetAll();

    List<CommandAndResponseDto> CreateOrUpdtateAll(List<CommandAndResponseDto> commands);

    bool Delete(List<CommandAndResponseDto> commands);
}