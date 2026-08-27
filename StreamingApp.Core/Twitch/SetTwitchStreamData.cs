using StreamingApp.API.Interfaces;
using StreamingApp.Domain.Entities.APIs;

namespace StreamingApp.Core.Twitch;

public class SetTwitchStreamData
{
    private readonly ITwitchSendRequest _twitchSendRequest;

    public SetTwitchStreamData(ITwitchSendRequest twitchSendRequest)
    {
        _twitchSendRequest = twitchSendRequest;
    }

    public async Task<bool> Execute(ChannelInfo StreamData)
    {
        return _twitchSendRequest.SetChannelInfo(StreamData.GameId, StreamData.Title);
    }
}
