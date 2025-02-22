using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface ICRUDSpecialWords
{
    List<SpecialWordDto> GetAll();

    List<SpecialWordDto> CreateOrUpdtateAll(List<SpecialWordDto> specialWords);

    bool Delete(List<SpecialWordDto> specialWords);
}