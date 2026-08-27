using StreamingApp.API.Interfaces;
using StreamingApp.Domain.Entities.APIs;

namespace StreamingApp.Core.Twitch;

public class GetTwitchStreamData
{
    private readonly ITwitchSendRequest _twitchSendRequest;

    public GetTwitchStreamData(ITwitchSendRequest twitchSendRequest    )
    {
        _twitchSendRequest = twitchSendRequest;
    }

    public async Task<ChannelInfo> Execute()
    {
        return await _twitchSendRequest.GetChannelInfo(null, true);
    }
}
