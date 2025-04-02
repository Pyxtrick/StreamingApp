using StreamingApp.API.Utility.Caching.CacheData;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Enums;

namespace StreamingApp.API.Utility.Caching;

public class EmotesCache : IEmotesCache
{
    private readonly EmotesCacheData _emotesCacheData;

    public EmotesCache(EmotesCacheData emotesCacheData)
    {
        _emotesCacheData = emotesCacheData;
    }

    public void AddEmotes(List<EmoteDto> emotes)
    {
        var data = _emotesCacheData.EmoteData;

        if (data == null)
        {
            _emotesCacheData.EmoteData = emotes;
            return;
        }

        foreach (var emote in emotes)
        {
            if (data.Contains(emote) == false)
            {
                _emotesCacheData.EmoteData.Add(emote);
            }
        }
    }

    public List<EmoteDto> GetEmotes(EmoteProviderEnum? emoteProviderEnum)
    {
        if (emoteProviderEnum == null)
        {
            return _emotesCacheData.EmoteData;
        }
        else
        {
            return _emotesCacheData.EmoteData.Where(e => e.Provider == emoteProviderEnum).ToList();
        }
    }
}
