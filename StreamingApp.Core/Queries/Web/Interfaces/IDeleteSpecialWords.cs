using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface IDeleteSpecialWords
{
    bool Delete(List<SpecialWordDto> specialWords);
}