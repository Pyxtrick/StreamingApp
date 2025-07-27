namespace StreamingApp.API.BetterTV_7TV;

public interface IEmotesApiRequest
{
    Task GetTVEmoteSet(string? channelId);
}