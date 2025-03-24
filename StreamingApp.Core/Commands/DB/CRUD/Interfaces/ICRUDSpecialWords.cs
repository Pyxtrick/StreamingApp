using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDSpecialWords
{
    Task<List<SpecialWordDto>> GetAll();

    Task<List<SpecialWordDto>> CreateOrUpdtateAll(List<SpecialWordDto> specialWords);

    Task<bool> Delete(List<SpecialWordDto> specialWords);
}