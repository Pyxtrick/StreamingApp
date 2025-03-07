using StreamingApp.Domain.Entities.APIs;

namespace StreamingApp.API.Interfaces;
public interface ISendRequest
{
    Task<ChannelInfo?> GetChannelInfo(string? broadcasterId);
    void SendChatMessage(string message);

    void SendResplyChatMessage(string message, string replyToId);
    bool SetChannelInfo(string? gameId, string? title);

    void SendAnnouncement(string message);

    Task DeleteMessage(string messageId);

    Task TimeoutUser(string userId, string reson, int time);
}