using Microsoft.Extensions.Configuration;
using Moq;
using StreamingApp.API.Twitch;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Test.TestBuilder;
using TwitchLib.Client.Events;
using Xunit;

namespace StreamingApp.Test.API.Twitch;

public class TwitchApiRequestFixture
{
    [Fact]
    public async Task OnHypeTrain()
    {
        // Arrange
        Mock<ITwitchCache> twitchCacheMock = new();
        Mock<ITwitchCallCache> twitchCallCacheMock = new();

        Dictionary<string, string> inMemorySettings = new()
            {
                { "Twitch:Channel", "Pyxtrick" },
            };

        IConfiguration configurationMock = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

        ITwitchApiRequest twitchApiRequest = CreateTwitchApiRequest(twitchCacheMock, twitchCallCacheMock, configurationMock);

        // Act


        // Assert

    }

    [Fact]
    public async Task OnSendReceiveData()
    {
        // Arrange
        Mock<ITwitchCache> twitchCacheMock = new();
        Mock<ITwitchCallCache> twitchCallCacheMock = new();
        IConfiguration configurationMock = new ConfigurationBuilder().Build();

        ITwitchApiRequest twitchApiRequest = CreateTwitchApiRequest(twitchCacheMock, twitchCallCacheMock, configurationMock);

        Object sender = new Object();
        OnSendReceiveDataArgs data = new() { Data = "@badge-info=founder/9;badges=moderator/1,founder/0;color=#FF0000;display-name=NoodleSnekBot;emotes=emotesv2_766a575b6d714bc1a5f0d09edd5dc69e:0-11;flags=;id=cf2b9fa9-1bef-4d79-912a-1e7e81181453;login=noodlesnekbot;mod=1;msg-id=viewermilestone;msg-param-category=watch-streak;msg-param-copoReward=450;msg-param-id=c471db8b-7505-4c11-a0f8-4a8234c447da;msg-param-value=50;room-id=66716756;subscriber=1;system-msg=NoodleSnekBot\\swatched\\s50\\sconsecutive\\sstreams\\sand\\ssparked\\sa\\swatch\\sstreak!;tmi-sent-ts=1771617486695;user-id=1039826911;user-type=mod;vip=0 :tmi.twitch.tv USERNOTICE #pyxtrick :pyxtriFlower" };

        // Act & Assert
        twitchApiRequest.Bot_OnSendReceiveData(sender, data);
    }

    private ITwitchApiRequest CreateTwitchApiRequest(IMock<ITwitchCache> twitchCache, IMock<ITwitchCallCache> twitchCallCache, IConfiguration configuration)
    {
        return new TwitchApiRequest(twitchCache.Object, configuration, twitchCallCache.Object, MapperBuilder.CreateMapperConfig());
    }
}
