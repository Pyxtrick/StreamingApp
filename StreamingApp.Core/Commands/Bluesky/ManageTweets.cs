using idunno.Bluesky;
using idunno.Bluesky.RichText;
using StreamingApp.API.Bluesky.Interfaces;
using StreamingApp.API.Interfaces;
using StreamingApp.Domain.Enums;
using System.Text.RegularExpressions;

namespace StreamingApp.Core.Commands.Bluesky;

public class ManageTweets : IManageTweets
{
    private readonly IBlueskyApiRequest _blueskyApiRequest;

    private readonly ITwitchSendRequest _twitchSendRequest;

    public ManageTweets(IBlueskyApiRequest blueskyApiRequest, ITwitchSendRequest twitchSendRequest)
    {
        _blueskyApiRequest = blueskyApiRequest;
        _twitchSendRequest = twitchSendRequest;
    }

    public async Task SendBasicTweet(string message)
    {
        var t = Regex.Unescape(message);

        await _blueskyApiRequest.PostTweet(t);
    }

    public async Task SendStreamStartTweet(List<KeyValuePair<OriginEnum, string>> services)
    {
        var channelInfo = await _twitchSendRequest.GetChannelInfo("", true);
        PostBuilder postBuilder = new PostBuilder();

        string tweetIntro = $"{channelInfo.Title} | {channelInfo.GameName}\r\n 🔴Live\r\n";
        string tweetMessage = tweetIntro;
        postBuilder.Append(tweetIntro);

        foreach (var service in services)
        {
            var originMessage = OriginLookupText(service.Key);

            tweetMessage += $"{originMessage}{service.Value} \r\n";

            postBuilder.Append(new Link($"{originMessage}{service.Value}"));
            postBuilder.Append("\r\n");
            Console.WriteLine(postBuilder);
        }

        tweetMessage += "#Vtuber #VtuberEN";
        postBuilder.Append(new HashTag("#Vtuber"));
        postBuilder.Append(new HashTag("#VtuberEN"));

        await _blueskyApiRequest.PostTweet(postBuilder);
    }

    // Move to DB in the future
    private string OriginLookupText(OriginEnum originEnum)
    {
        switch (originEnum)
        {
            case OriginEnum.Twitch:
                return "https://twitch.tv/pyxtrick";
            case OriginEnum.Youtube:
                return "https://youtube.com/watch?v=";
            case OriginEnum.Kick:
                return "https://kick.com/pyxtrick";
            default:
                return "";
        }
    }
}
