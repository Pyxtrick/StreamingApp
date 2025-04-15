using StreamingApp.API.Interfaces;
using StreamingApp.API.StreamerBot;
using StreamingApp.Domain.Entities.APIs;

namespace StreamingApp.API.Twitch;

public class YoutubeSendRequest : IYouTubeSendRequest
{
    private readonly IStreamerBotRequest _streamerBotRequest;

    public YoutubeSendRequest(IStreamerBotRequest streamerBotRequest)
    {
        _streamerBotRequest = streamerBotRequest;
    }

    public async Task DeleteMessage(string messageId)
    {
        var data = new List<KeyValuePair<string, string>> { new("messageId", messageId) };

        await _streamerBotRequest.DoAction("", "", data);
    }

    public async Task<ChannelInfo?> GetChannelInfo(string? broadcasterId)
    {
        var data = new List<KeyValuePair<string, string>> { new("broadcasterId", broadcasterId) };

        await _streamerBotRequest.DoAction("", "", data);

        return new ChannelInfo();
    }

    public void SendAnnouncement(string message)
    {
        var data = new List<KeyValuePair<string, string>> { new("rawInput", message) };

        _streamerBotRequest.DoAction("", "", data);
    }

    public void SendChatMessage(string message)
    {
        var data = new List<KeyValuePair<string, string>> {new("rawInput", message)};

        _streamerBotRequest.DoAction("3f953f7d-124f-42cf-9e97-751c60fb60c6", "Scheduled Message", data);
    }

    public void SendResplyChatMessage(string message, string replyToId)
    {
        var data = new List<KeyValuePair<string, string>> { new("rawInput", message), new("replyTo", replyToId) };

        _streamerBotRequest.DoAction("", "", data);
    }

    public bool SetChannelInfo(string? gameId, string? title)
    {
        var data = new List<KeyValuePair<string, string>> { new("gameId", gameId), new("title", title) };

        _streamerBotRequest.DoAction("", "", data);

        return true;
    }

    public async Task TimeoutUser(string userId, string reson, int time)
    {
        var data = new List<KeyValuePair<string, string>> { new("userId", userId), new("reson", reson), new("time", time.ToString()) };

        await _streamerBotRequest.DoAction("", "", data);
    }
}
