using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.API.Interfaces;

public interface ITwitchSendRequest : ISendRequest;
public interface IYouTubeSendRequest : ISendRequest;

public interface ISendRequest
{
    Task<UserDto?> GetUser(string? userName);
    Task<ChannelInfo?> GetChannelInfo(string? broadcasterId, bool isId);
    void SendChatMessage(string message);

    void SendResplyChatMessage(string message, string replyToId);
    bool SetChannelInfo(string? gameId, string? title);

    void SendAnnouncement(string message);

    Task DeleteMessage(string messageId);

    Task TimeoutUser(string userId, string reson, int time);
}