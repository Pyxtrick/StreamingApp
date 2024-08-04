using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface IGetSpecialWords
{
    List<SpecialWordDto> GetAll();
}