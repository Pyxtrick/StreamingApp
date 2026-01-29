using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB.Stream;

namespace StreamingApp.API.Interfaces;

public interface ITwitchSendRequest : ISendRequest;
public interface IYouTubeSendRequest : ISendRequest;

public interface ISendRequest
{
    Task<UserDto?> GetUser(string? userName);
    Task<ChannelInfo?> GetChannelInfo(string? broadcasterId, bool isId);
    Task SendChatMessage(string message);

    Task SendReplyChatMessage(string message, string replyToId);
    bool SetChannelInfo(string? gameId, string? title);

    void SendAnnouncement(string message);

    Task<StreamHighlight> CreateClip(string message);

    Task DeleteMessage(string messageId);

    Task TimeoutUser(string userId, string reson, int time);
}