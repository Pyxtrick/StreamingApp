﻿using StreamingApp.API.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Internal.Stream;
using TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation;
using TwitchLib.Api.Helix.Models.Polls.CreatePoll;
using TwitchLib.Api.Helix.Models.Predictions.CreatePrediction;

namespace StreamingApp.API.Twitch;

public class TwitchSendRequest : ISendRequest
{
    private readonly ITwitchCache _twitchCache;

    public TwitchSendRequest(ITwitchCache twitchCache)
    {
        _twitchCache = twitchCache;
    }

    /// <summary>
    /// Channel Info with GameId, GameName, Title
    /// </summary>
    /// <returns>ChannelInfo</returns>
    public async Task<ChannelInfo?> GetChannelInfo()
    {
        var channel = await _twitchCache.GetTheTwitchAPI().Helix.Channels.GetChannelInformationAsync("broadcasterId");

        if (channel != null)
        {
            var info = new ChannelInfo()
            {
                GameId = channel.Data.FirstOrDefault().GameId,
                GameName = channel.Data.FirstOrDefault().GameName,
                Title = channel.Data.FirstOrDefault().Title,
            };
            return info;
        }

        return null;
    }

    /// <summary>
    /// Send Twtich Message
    /// </summary>
    /// <param name="message"></param>
    public void SendChatMessage(string message)
    {
        _twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), message);
    }

    /// <summary>
    /// Set Category and Title
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="title"></param>
    /// <returns>Success Bool</returns>
    public bool SetChannelInfo(string gameId, string title)
    {
        try
        {
            var t = new ModifyChannelInformationRequest()
            {
                GameId = gameId ?? null,
                Title = title ?? null,
                BroadcasterLanguage = null,
                Delay = null,
            };

            _twitchCache.GetTheTwitchAPI().Helix.Channels.ModifyChannelInformationAsync(_twitchCache.GetTwitchChannelName(), t);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Creates a Pole
    /// </summary>
    /// <param name="title"></param>
    /// <param name="options"></param>
    /// <param name="time"></param>
    /// <param name="pole"></param>
    /// <returns></returns>
    public async Task<Pole?> CreatePoleOrPrediction(string title, string[] options, int time, bool pole)
    {
        if (pole)
        {
            var pollRequest = new CreatePollRequest()
            {
                BroadcasterId = _twitchCache.GetTwitchChannelName(),
                Title = title ?? null,
                Choices = options.Select(option => { return new TwitchLib.Api.Helix.Models.Polls.CreatePoll.Choice() { Title = option }; }).ToList().ToArray(),
                BitsVotingEnabled = false,
                ChannelPointsVotingEnabled = false,
                DurationSeconds = time,
            };

            var response = await _twitchCache.GetTheTwitchAPI().Helix.Polls.CreatePollAsync(pollRequest);

            if (response.Data.Any())
            {

                return new Pole()
                {
                    PoleId = response.Data.LastOrDefault().Id,
                    IsPole = pole,
                };
            }
            return null;
        }
        else
        {
            var predictionRequest = new CreatePredictionRequest()
            {
                BroadcasterId = _twitchCache.GetTwitchChannelName(),
                Title=title ?? null,
                Outcomes = options.Select(option => { return new Outcome() { Title = option }; }).ToList().ToArray(),
                PredictionWindowSeconds = time,
            };

            var response = await _twitchCache.GetTheTwitchAPI().Helix.Predictions.CreatePredictionAsync(predictionRequest);

            if (response.Data.Any())
            {
                return new Pole()
                {
                    PoleId = response.Data.LastOrDefault().Id,
                    IsPole = pole,
                };
            }
            return null;
        }
    }

    /// <summary>
    /// Get Last Poles
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pole"></param>
    /// <returns></returns>
    public async Task<Pole?> GetPoleOrPrediction(string id, bool pole)
    {
        if (pole)
        {
            var t = await _twitchCache.GetTheTwitchAPI().Helix.Polls.GetPollsAsync(_twitchCache.GetTwitchChannelName(), new List<string>() { id });

            var data = t.Data.Last();

            return new Pole()
            {
                PoleId = data.Id,
                IsPole = pole,
                Title = data.Title,
                StartedAt = data.StartedAt,
                Choices = data.Choices.Select(option => { return new Domain.Entities.Internal.Stream.Choice() { Title = option.Title, Votes = option.Votes, ChannelPointsVotes = option.ChannelPointsVotes, BitsVotes = option.ChannelPointsVotes }; }).ToList(),
            };
        }
        else
        {
            var t = await _twitchCache.GetTheTwitchAPI().Helix.Predictions.EndPredictionAsync(_twitchCache.GetTwitchChannelName(), id, TwitchLib.Api.Core.Enums.PredictionEndStatus.RESOLVED);

            var data = t.Data.Last();

            return new Pole()
            {
                PoleId = data.Id,
                IsPole = pole,
                Title = data.Title,
                StartedAt = DateTime.Parse(data.CreatedAt),
                Choices = data.Outcomes.Select(option => { return new Domain.Entities.Internal.Stream.Choice() { Title = option.Title, Votes = option.ChannelPointsVotes, VotesPoints = option.ChannelPointsVotes, ChannelPointsVotes = option.ChannelPointsVotes, BitsVotes = option.ChannelPointsVotes }; }).ToList(),
            };
        }
    }

    public void GetEmotes()
    {
        var emotes = _twitchCache.GetOwnerOfChannelConnection().ChannelEmotes;
    }

    public async Task RaidChannel()
    {
        string fromBroadcasterId = _twitchCache.GetTwitchChannelName();
        string toBroadcasterId = "";

        var t = _twitchCache.GetTheTwitchAPI().Helix.Search.SearchChannelsAsync(toBroadcasterId, true);

        await _twitchCache.GetTheTwitchAPI().Helix.Raids.StartRaidAsync(fromBroadcasterId, toBroadcasterId);
    }

    public async Task GetHypeTrain()
    {
        var t = await _twitchCache.GetTheTwitchAPI().Helix.HypeTrain.GetHypeTrainEventsAsync(_twitchCache.GetTwitchChannelName());
    }

    public async Task SendAnnouncement()
    {
        //Send Chat Announcement
    }

    public async Task SendShoutout()
    {
        //Send a Shoutout
    }

    public async Task AdSchedule()
    {
        //GET https://api.twitch.tv/helix/channels/ads
        // https://dev.twitch.tv/docs/api/reference/#get-ad-schedule
        //_twitchCache.GetTheTwitchAPI().Helix.Channels.ads
    }

    public async Task UpdateCustomReward()
    {
        //var t = await _twitchCache.GetTheTwitchAPI().Helix.ChannelPoints.UpdateCustomRewardAsync();
    }

    /// <summary>
    /// Warn user with reason
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="reason"></param>
    /// <returns></returns>
    public async Task WarnUser(string userId, string reason)
    {
        // TODO: Warn Chat User
        // https://dev.twitch.tv/docs/api/reference/#warn-chat-user
        // POST https://api.twitch.tv/helix/moderation/warnings
    }

    /// <summary>
    /// Delete Chat Message
    /// </summary>
    /// <returns></returns>
    public async Task DeleteMessage()
    {
        // TODO: Delete Message (maybe with Reason)
    }

    /// <summary>
    /// Timeout User for x Seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public async Task TimeoutUser(int time)
    {
        // TODO: Timeout user with amount in Seconds (maybe with Reson)
    }

    /// <summary>
    /// Ban User with Reson
    /// </summary>
    /// <param name="reson"></param>
    /// <returns></returns>
    public async Task BanUser(string reson)
    {
        // TODO: Ban User with a specific Reson
    }
}
