using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDStreams
{
    Task<List<StreamDto>> GetAll();

    Task<List<StreamDto>> CreateOrUpdtateAll(List<StreamDto> streams);

    Task<bool> Delete(List<StreamDto> streams);
}