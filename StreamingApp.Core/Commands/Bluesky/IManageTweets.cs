using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Bluesky;

public interface IManageTweets
{
    Task SendBasicTweet(string message);
    Task SendStreamStartTweet(List<KeyValuePair<OriginEnum, string>> services);
}