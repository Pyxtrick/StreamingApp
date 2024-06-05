namespace StreamingApp.API.BetterTV_7TV;

public class BetterTVApiRequest
{
    private string UriAPI = "https://api.betterttv.net/3";
    private string UriWS = "wss://sockets.betterttv.net/ws";

    private string Provider = "Twitch"; // Diffrent for Youtube
    private string ProviderId = "66716756"; // Diffrent for Youtube

    public void GetBttvGlobalEmotes()
    {
        string Uri = $"{UriAPI}/cached/emotes/global";

        /**BetterTVEmotes response = ;

        List<EmoteDto> emotes = new List<EmoteDto>();

        foreach (_7TVEmote _7emote in response.emotes)
        {
            EmoteDto emote = new EmoteDto(_7emote.Id, _7emote.name, $"https://cdn.7tv.app/emote/{_7emote.Id}");

            emotes.Add(emote);
        }**/
    }

    public void GetBttvUserEmotes()
    {
        string Uri = $"{UriAPI}/cached/users/{Provider}/{ProviderId}";
    }

    public void GetBttvBadges()
    {
        string Uri = $"{UriAPI}/cached/badges/{Provider}";
    }
}

