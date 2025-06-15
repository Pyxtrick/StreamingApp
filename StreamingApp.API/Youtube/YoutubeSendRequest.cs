using StreamingApp.API.Interfaces;
using StreamingApp.API.StreamerBot;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Enums;

namespace StreamingApp.API.Twitch;

public class YoutubeSendRequest : IYouTubeSendRequest
{
    private readonly IStreamerBotRequest _streamerBotRequest;

    private readonly UnitOfWorkContext _unitOfWork;

    public YoutubeSendRequest(IStreamerBotRequest streamerBotRequest, UnitOfWorkContext unitOfWorkContext)
    {
        _streamerBotRequest = streamerBotRequest;
        _unitOfWork = unitOfWorkContext;
    }

    public async Task<UserDto?> GetUser(string? userName)
    {
        await _streamerBotRequest.DoAction("", "", new() { new("messageId", userName) });

        return null;
    }

    public async Task DeleteMessage(string messageId)
    {
        await _streamerBotRequest.DoAction("", "", new() { new("messageId", messageId) });
    }

    public async Task<ChannelInfo?> GetChannelInfo(string? broadcasterId, bool isId)
    {
        await _streamerBotRequest.DoAction("", "", new() { new("broadcasterId", broadcasterId) });

        return new ChannelInfo();
    }

    public void SendAnnouncement(string message)
    {
        var settings = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == ChatOriginEnum.Youtube);

        if (settings.PauseChatMessages == false)
        {
            var data = new List<KeyValuePair<string, string>> { new("rawInput", message) };

            _streamerBotRequest.DoAction("", "", new() { new("rawInput", message) });
        }
    }

    public void SendChatMessage(string message)
    {
        var settings = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == ChatOriginEnum.Youtube);

        if (settings.PauseChatMessages == false)
        {
            _streamerBotRequest.DoAction("3f953f7d-124f-42cf-9e97-751c60fb60c6", "Scheduled Message", new() { new("rawInput", message) });
        }
    }

    public void SendResplyChatMessage(string message, string replyToId)
    {
        var settings = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == ChatOriginEnum.Youtube);

        if (settings.PauseChatMessages == false)
        {
            var data = new List<KeyValuePair<string, string>> { new("rawInput", message), new("replyTo", replyToId) };

            _streamerBotRequest.DoAction("", "", new() { new("rawInput", message), new("replyTo", replyToId) });
        }
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
