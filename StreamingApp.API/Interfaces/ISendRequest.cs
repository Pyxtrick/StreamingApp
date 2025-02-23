using StreamingApp.Domain.Entities.APIs;

namespace StreamingApp.API.Interfaces;
public interface ISendRequest
{
    Task<ChannelInfo?> GetChannelInfo(string? broadcasterId);
    void SendChatMessage(string message);
    bool SetChannelInfo(string? gameId, string? title);
}