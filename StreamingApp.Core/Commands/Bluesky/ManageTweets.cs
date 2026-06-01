using idunno.Bluesky;
using idunno.Bluesky.RichText;
using StreamingApp.API.Bluesky.Interfaces;
using StreamingApp.API.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Enums;
using System.Text.RegularExpressions;

namespace StreamingApp.Core.Commands.Bluesky;

public class ManageTweets : IManageTweets
{
    private readonly IBlueskyApiRequest _blueskyApiRequest;

    private readonly ITwitchSendRequest _twitchSendRequest;

    private readonly UnitOfWorkContext _unitOfWork;

    public ManageTweets(IBlueskyApiRequest blueskyApiRequest, ITwitchSendRequest twitchSendRequest, UnitOfWorkContext unitOfWorkContext)
    {
        _blueskyApiRequest = blueskyApiRequest;
        _twitchSendRequest = twitchSendRequest;
        _unitOfWork = unitOfWorkContext;
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

        var setting = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == OriginEnum.Twitch);

        string tweetIntro = $"{channelInfo.Title}";
        tweetIntro += setting.UseGameName ? $" | {channelInfo.GameName}" : "";
        tweetIntro += "\r\n🔴Live\r\n";
        string tweetMessage = tweetIntro;
        postBuilder.Append(tweetIntro);

        foreach (var service in services)
        {
            var originMessage = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == service.Key).StreamLink;

            tweetMessage += $"{originMessage}{service.Value} \r\n";

            postBuilder.Append(new Link($"{originMessage}{service.Value}"));
            postBuilder.Append("\r\n");
            Console.WriteLine(postBuilder);
        }

        tweetMessage += "#Vtuber #VtuberEN";
        postBuilder.Append(new HashTag("Vtuber"));
        postBuilder.Append(" ");
        postBuilder.Append(new HashTag("VtuberEN"));

        await _blueskyApiRequest.PostTweet(postBuilder);
    }
}
