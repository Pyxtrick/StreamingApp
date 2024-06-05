namespace StreamingApp.API.BetterTV_7TV;

public class _7TVApiRequest
{
    private string UriAPI = "https://7tv.io/v3";
    private string UriWS = "";

    private string UserID = "64ea55d417752b50e91f1544"; // Pyxtrick
    private string EmoteSetId = "653ffcc86efaa2fce99fc2df"; // Pyxtrick Emotes

    private string Provider = "Twitch"; // Diffrent for Youtube
    private string ProviderId = "66716756"; // Diffrent for Youtube

    // TODO: refresh list when new emote is added
    // TODO: Refresh by api request

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

    public void GetTVEmoteSet()
    {
        string Uri = $"{UriAPI}/emote-sets/{EmoteSetId}";

        /**_7TVEmoteList response = ;
        
        List<EmoteDto> emotes = new List<EmoteDto>();

        foreach (_7TVEmote _7emote in response.emotes)
        {
            EmoteDto emote = new EmoteDto(_7emote.Id, _7emote.name, $"https://cdn.7tv.app/emote/{_7emote.Id}");

            emotes.Add(emote);
        }**/
    }
}
