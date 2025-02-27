using StreamingApp.API.Interfaces;
using StreamingApp.Domain.Entities.APIs;

namespace StreamingApp.API.Twitch;

public class YoutubeSendRequest : ISendRequest
{
    /// <summary>
    /// Channel Info with GameId, GameName, Title
    /// </summary>
    /// <returns>ChannelInfo</returns>
    public async Task<ChannelInfo?> GetChannelInfo(string? broadcasterId)
    {
        return null;
    }

    /// <summary>
    /// Send Youtube Message
    /// </summary>
    /// <param name="message"></param>
    public void SendChatMessage(string message)
    {
        // TODO: implement
    }

    /// <summary>
    /// Set Category and Title
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="title"></param>
    /// <returns>Success Bool</returns>
    public bool SetChannelInfo(string gameId, string title)
    {
        // TODO: implement
        return false;
    }

    public void SendAnnouncement(string message)
    {
        // TODO: implement
    }
}
