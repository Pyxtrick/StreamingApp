﻿using TwitchLib.Api;
using TwitchLib.Client;

namespace StreamingApp.API.Utility.Caching.Interface;

public interface ITwitchCache
{
    void AddData(TwitchClient twitchClient, TwitchAPI TheTwitchAPI);
    void AddTwitchChannelName(string channelName);
    TwitchClient GetOwnerOfChannelConnection();
    TwitchAPI GetTheTwitchAPI();
    string GetTwitchChannelName();
}