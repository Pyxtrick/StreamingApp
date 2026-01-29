using TwitchLib.Api;
using TwitchLib.Client;

namespace StreamingApp.API.Utility.Caching.Interface;

public interface ITwitchCache
{
    void AddData(TwitchClient twitchClient, TwitchAPI TheTwitchAPI, TwitchAPI AppTwitchAPI);
    TwitchClient GetOwnerOfChannelConnection();
    TwitchAPI GetTheTwitchAPI();

    TwitchAPI GetAppTwitchAPI();
}