using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Enums;

namespace StreamingApp.API.Utility.Caching.Interface;
public interface IEmotesCache
{
    void AddEmotes(List<EmoteDto> emotes);
    List<EmoteDto> GetEmotes(EmoteProviderEnum? emoteProviderEnum);
}