using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web.Interfaces;
public interface IUpdateSpecialWords
{
    List<SpecialWordDto> CreateOrUpdtateAll(List<SpecialWordDto> specialWords);
}