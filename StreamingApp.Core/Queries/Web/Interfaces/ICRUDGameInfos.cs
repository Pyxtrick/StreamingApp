using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface ICRUDGameInfos
{
    List<GameInfoDto> GetAll();

    List<GameInfoDto> CreateOrUpdtateAll(List<GameInfoDto> gameInfos);

    bool Delete(List<GameInfoDto> gameInfos);
}