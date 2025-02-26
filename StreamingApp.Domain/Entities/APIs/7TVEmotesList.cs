using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.APIs;

public class _7TVEmoteList
{
    //0: Twitch, 1: 7TV, 2: BetterTTV, 3: FrankerFaceZ
    public EmoteProviderEnum provider { get; set; }

    public string code { get; set; }

    public List<_7TVEmote>? urls { get; set; }
}
