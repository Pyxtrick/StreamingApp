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
        await _streamerBotRequest.DoAction("", "", new() { new("messageId", messageId) });
    }

    public async Task<ChannelInfo?> GetChannelInfo(string? broadcasterId)
    {
        await _streamerBotRequest.DoAction("", "", new() { new("broadcasterId", broadcasterId) });

        return new ChannelInfo();
    }

    public void SendAnnouncement(string message)
    {
        var data = new List<KeyValuePair<string, string>> { new("rawInput", message) };

        _streamerBotRequest.DoAction("", "", new() { new("rawInput", message) });
    }

    public void SendChatMessage(string message)
    {
        _streamerBotRequest.DoAction("3f953f7d-124f-42cf-9e97-751c60fb60c6", "Scheduled Message", new() { new("rawInput", message) });
    }

    public void SendResplyChatMessage(string message, string replyToId)
    {
        var data = new List<KeyValuePair<string, string>> { new("rawInput", message), new("replyTo", replyToId) };

        _streamerBotRequest.DoAction("", "", new() { new("rawInput", message), new("replyTo", replyToId) });
    }

    public bool SetChannelInfo(string? gameId, string? title)
    {
        _streamerBotRequest.DoAction("", "", new() { new("gameId", gameId), new("title", title) });

        return true;
    }

    public async Task TimeoutUser(string userId, string reson, int time)
    {
        await _streamerBotRequest.DoAction("", "", new() { new("userId", userId), new("reson", reson), new("time", time.ToString()) });
    }
}
