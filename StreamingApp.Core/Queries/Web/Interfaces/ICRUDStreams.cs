using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface ICRUDStreams
{
    List<StreamDto> GetAll();

    List<StreamDto> CreateOrUpdtateAll(List<StreamDto> streams);

    bool Delete(List<StreamDto> streams);
}