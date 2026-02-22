using idunno.Bluesky;
using Moq;
using StreamingApp.API.Bluesky.Interfaces;
using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Bluesky;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Enums;
using Xunit;

namespace StreamingApp.Test.Core.Command.Bluesky;

public class ManageTweetsFixture
{
    [Fact]
    public async Task SendStreamStartTweet()
    {
        //Arrange
        List<KeyValuePair<OriginEnum, string>> servicesParameters = new List<KeyValuePair<OriginEnum, string>>() { new(OriginEnum.Twitch, "") };

        Mock<IBlueskyApiRequest> blueskyApiRequestMock = new();
        blueskyApiRequestMock.Setup(blueskyApiRequest => blueskyApiRequest.PostTweet(It.IsAny<PostBuilder>()));

        var channelinfo = new ChannelInfo() { GameId = "111", GameName = "TestGame", Title = "TestTitle" };

        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
        twitchSendRequestMock.Setup(twitchSendRequest => twitchSendRequest.GetChannelInfo("", true)).ReturnsAsync(channelinfo);

        IManageTweets manageTweets = new ManageTweets(blueskyApiRequestMock.Object, twitchSendRequestMock.Object);

        //Act
        await manageTweets.SendStreamStartTweet(servicesParameters);

        //Assert
        blueskyApiRequestMock.Verify(blueskyApiRequest => blueskyApiRequest.PostTweet(It.IsAny<PostBuilder>()), Times.Once);
        twitchSendRequestMock.Verify(twitchSendRequest => twitchSendRequest.GetChannelInfo("", true), Times.Once);
    }
}
