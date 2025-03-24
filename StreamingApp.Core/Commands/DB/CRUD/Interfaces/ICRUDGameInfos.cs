using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDGameInfos
{
    Task<List<GameInfoDto>> GetAll();

    Task<List<GameInfoDto>> CreateOrUpdtateAll(List<GameInfoDto> gameInfos);

    Task<bool> Delete(List<GameInfoDto> gameInfos);
}