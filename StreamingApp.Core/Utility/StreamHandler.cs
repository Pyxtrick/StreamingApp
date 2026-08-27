using StreamingApp.API.Bluesky.Interfaces;
using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Bluesky;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Utility;

public class StreamHandler : IStreamHandler
{
    private readonly IManageStream _manageTwitchStream;

    private readonly IManageTweets _manageTweets;

    private readonly IBlueskyApiRequest _blueskyApiRequest;

    private readonly ITwitchSendRequest _twitchSendRequest;

    public StreamHandler(IManageStream manageTwitchStream, IManageTweets manageTweets, IBlueskyApiRequest blueskyApiRequest, ITwitchSendRequest twitchSendRequest)
    {
        _manageTwitchStream = manageTwitchStream;
        _manageTweets = manageTweets;
        _blueskyApiRequest = blueskyApiRequest;
        _twitchSendRequest = twitchSendRequest;
    }

    public async Task<bool> Start(string youtubeId = "")
    {
        List<KeyValuePair<OriginEnum, string>> value = new List<KeyValuePair<OriginEnum, string>>() { new(OriginEnum.Twitch, "") };

        if (!string.IsNullOrWhiteSpace(youtubeId))
        {
            await _manageTwitchStream.StartStream(youtubeId);

            value.Add(new(OriginEnum.Youtube, youtubeId));
        }

        await _manageTweets.SendStreamStartTweet(value);

        var channelInfo = await _twitchSendRequest.GetChannelInfo("", true);
        await _blueskyApiRequest.SetLiveStatus($"{channelInfo.Title} | {channelInfo.GameName}", $"Pyxtrick Livestream of {channelInfo.GameName}", 240, true);


        // TODO: Discord Notification
        // TODO: Twitter Notification

        return true;
    }

    public async Task<bool> Stop()
    {
        await _manageTwitchStream.EndStream();

        await _blueskyApiRequest.SetLiveStatus("", "", 0, false);

        return true;
    }
}
