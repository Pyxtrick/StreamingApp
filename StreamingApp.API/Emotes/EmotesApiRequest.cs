using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.API.BetterTV_7TV;

public class EmotesApiRequest : IEmotesApiRequest
{
    private readonly IConfiguration _configuration;

    private readonly IEmotesCache _emotesCache;

    /**
     * 1x.avif | 32 x 32
     * 1x.webp | 32 x 32
     * 2x.avif | 64 x 64
     * 2x.webp | 64 x 64
     * 3x.avif | 96 x 96
     * 3x.webp | 96 x 96
     * 4x.avif | 128 x 128
     * 4x.webp | 128 x 128
     */

    public EmotesApiRequest(IConfiguration configuration, IEmotesCache emotesCache)
    {
        _configuration = configuration;
        _emotesCache = emotesCache;
    }

    public async Task GetTVEmoteSet()
    {
        string services = "7tv.bttv.ffz";

        //https://adiq.stoplight.io/docs/temotes/a2ff59cc81676-get-channel-emotes
        HttpResponseMessage response = await new HttpClient().GetAsync($"https://emotes.adamcy.pl/v1/channel/{_configuration["Twitch:Channel"]}/emotes/{services}");
        string stringResponse = await response.Content.ReadAsStringAsync();
        List<_7TVEmoteList> convertedResponse = JsonConvert.DeserializeObject<List<_7TVEmoteList>>(stringResponse);

        List<EmoteDto> emotes = new();

        foreach (_7TVEmoteList _7emote in convertedResponse)
        {
            // replase 1x.webp with 1-4 for a bigger image
            EmoteDto emote = new EmoteDto(_7emote.code, _7emote.provider, _7emote.code, _7emote.urls[0].url);

            emotes.Add(emote);
        }

        _emotesCache.AddEmotes(emotes);
    }
}
