using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamingApp.API.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB.Stream;
using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation;
using TwitchLib.Api.Helix.Models.Moderation.BanUser;
using TwitchLib.Api.Helix.Models.Polls.CreatePoll;
using TwitchLib.Api.Helix.Models.Predictions.CreatePrediction;
using WebSocketSharp;

namespace StreamingApp.API.Twitch;

public class TwitchSendRequest : ITwitchSendRequest
{
    private readonly ITwitchCache _twitchCache;
    private readonly ITwitchCallCache _twitchCallCache;

    private readonly IConfiguration _configuration;

    private readonly ILogger<TwitchSendRequest> _logger;

    private readonly UnitOfWorkContext _unitOfWork;

    public TwitchSendRequest(ITwitchCache twitchCache, ITwitchCallCache twitchCallCache, IConfiguration configuration, ILogger<TwitchSendRequest> logger, UnitOfWorkContext unitOfWork)
    {
        _twitchCache = twitchCache;
        _twitchCallCache = twitchCallCache;
        _configuration = configuration;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto?> GetUser(string? userName)
    {
        var usersResponse = await _twitchCache.GetTheTwitchAPI().Helix.Users.GetUsersAsync(null, new() { userName });

        var user = usersResponse.Users.FirstOrDefault();

        return new UserDto() { UserId = user.Id, UserName = user.DisplayName };
    }

    /// <summary>
    /// Channel Info with GameId, GameName, Title
    /// </summary>
    /// <param name="broadcasterId">null for using _twitchCache.ChannelId</param>
    /// <param name="isId">check for if twitchUserId is Used or UserName</param>
    /// <returns>ChannelInfo</returns>
    public async Task<ChannelInfo?> GetChannelInfo(string? broadcasterId, bool isId)
    {
        GetChannelInformationResponse channel = null;

        if (isId)
        {
            if (broadcasterId.IsNullOrEmpty())
            {
                broadcasterId = _configuration["Twitch:ChannelId"];
            }

            // TODO: Error
            // Your request was blocked due to bad credentials (Do you have the right scope for your access token?).
            // BroadcastID = "66716756"
            channel = await _twitchCache.GetTheTwitchAPI().Helix.Channels.GetChannelInformationAsync(broadcasterId);
        }
        else
        {
            var user = await GetUser(broadcasterId);

            channel = await _twitchCache.GetTheTwitchAPI().Helix.Channels.GetChannelInformationAsync(user.UserId);
        }

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
    /// Send Twitch Message
    /// </summary>
    /// <param name="message"></param>
    public void SendChatMessage(string message)
    {
        var settings = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == ChatOriginEnum.Twitch);

        if (settings.PauseChatMessages == false)
        {
            if (_twitchCache.GetOwnerOfChannelConnection().IsInitialized != false)
            {
                _twitchCache.GetOwnerOfChannelConnection().SendMessage(_configuration["Twitch:Channel"], message);
            }
        }
    }

    /// <summary>
    /// Send Twitch Reply Message
    /// </summary>
    /// <param name="message"></param>
    /// <param name="replyToId"></param>
    public void SendResplyChatMessage(string message, string replyToId)
    {
        var settings = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == ChatOriginEnum.Twitch);

        if (settings.PauseChatMessages == false)
        {
            _twitchCache.GetOwnerOfChannelConnection().SendReply(_configuration["Twitch:Channel"], replyToId, message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public void SendAnnouncement(string message)
    {
        var settings = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == ChatOriginEnum.Twitch);

        if (settings.PauseChatMessages == false)
        {
            //ChannelBotId
            _twitchCache.GetTheTwitchAPI().Helix.Chat.SendChatAnnouncementAsync(_configuration["Twitch:ChannelId"], _configuration["Twitch:ChannelBotId"], message);
        }
    }

    public async Task SendShoutout()
    {
        //Send a Shoutout
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public async Task CreateClip(string message)
    {
        var t = await _twitchCache.GetTheTwitchAPI().Helix.Clips.CreateClipAsync(_configuration["Twitch:ChannelId"]);
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

            _twitchCache.GetTheTwitchAPI().Helix.Channels.ModifyChannelInformationAsync(_configuration["Twitch:ChannelId"], t);

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
                BroadcasterId = _configuration["Twitch:ChannelId"],
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
                BroadcasterId = _configuration["Twitch:ChannelId"],
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
            var t = await _twitchCache.GetTheTwitchAPI().Helix.Polls.GetPollsAsync(_configuration["Twitch:ChannelId"], new List<string>() { id });

            var data = t.Data.Last();

            return new Pole()
            {
                PoleId = data.Id,
                IsPole = pole,
                Title = data.Title,
                StartedAt = data.StartedAt,
                Choices = data.Choices.Select(option => { return new Domain.Entities.InternalDB.Stream.Choice() { Title = option.Title, Votes = option.Votes, ChannelPointsVotes = option.ChannelPointsVotes, BitsVotes = option.ChannelPointsVotes }; }).ToList(),
            };
        }
        else
        {
            var t = await _twitchCache.GetTheTwitchAPI().Helix.Predictions.EndPredictionAsync(_configuration["Twitch:ChannelId"], id, TwitchLib.Api.Core.Enums.PredictionEndStatus.RESOLVED);

            var data = t.Data.Last();

            return new Pole()
            {
                PoleId = data.Id,
                IsPole = pole,
                Title = data.Title,
                StartedAt = DateTime.Parse(data.CreatedAt),
                Choices = data.Outcomes.Select(option => { return new Domain.Entities.InternalDB.Stream.Choice() { Title = option.Title, Votes = option.ChannelPointsVotes, VotesPoints = option.ChannelPointsVotes, ChannelPointsVotes = option.ChannelPointsVotes, BitsVotes = option.ChannelPointsVotes }; }).ToList(),
            };
        }
    }

    public void GetEmotes()
    {
        var emotes = _twitchCache.GetOwnerOfChannelConnection().ChannelEmotes;
    }

    public async Task RaidChannel()
    {
        string fromBroadcasterId = _configuration["Twitch:ChannelId"];
        string toBroadcasterId = "";

        var t = _twitchCache.GetTheTwitchAPI().Helix.Search.SearchChannelsAsync(toBroadcasterId, true);

        await _twitchCache.GetTheTwitchAPI().Helix.Raids.StartRaidAsync(fromBroadcasterId, toBroadcasterId);
    }

    public async Task GetChannelGoals()
    {
        var t = await _twitchCache.GetTheTwitchAPI().Helix.Goals.GetCreatorGoalsAsync(_configuration["Twitch:ChannelId"]);
    }

    public async Task GetChannelPoints()
    {
        var t = await _twitchCache.GetTheTwitchAPI().Helix.ChannelPoints.GetCustomRewardAsync(_configuration["Twitch:ChannelId"]);

        // TODO: Save to Cache
    }

    public async Task GetHypeTrain()
    {
        var t = await _twitchCache.GetTheTwitchAPI().Helix.HypeTrain.GetHypeTrainEventsAsync(_configuration["Twitch:ChannelId"]);
    }

    public async Task AdSchedule()
    {
        //TODO: User Streamer.bot or servy_bot for AD info

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
    public async Task DeleteMessage(string messageId)
    {
        // TODO: Delete Message (maybe with Reason)
        
        var messages = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedMessageData, true).ConvertAll(s => (string)s);

        _logger.LogCritical(messages.FirstOrDefault(m => m.Contains(messageId)));
        //await _twitchCache.GetTheTwitchAPI().Helix.Moderation.DeleteChatMessagesAsync(_configuration["Twitch:ChannelId"], _configuration["Twitch:ChannelBotId"], messageId);
    }

    /// <summary>
    /// Timeout User for x Seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public async Task TimeoutUser(string userId, string reson, int time)
    {
        // TODO: Timeout user with amount in Seconds (maybe with Reson)

        _logger.LogCritical($"UserId {userId} Reson: {reson} Time: {time}");
        // TODO: check if with Duration is a Timeout
        //await _twitchCache.GetTheTwitchAPI().Helix.Moderation.BanUserAsync(_configuration["Twitch:ChannelId"], _configuration["Twitch:ChannelBotId"], new BanUserRequest { UserId = userId, Reason = reson, Duration = time });
    }

    /// <summary>
    /// Ban User with Reson
    /// </summary>
    /// <param name="reson"></param>
    /// <returns></returns>
    public async Task BanUser(string userId, string reson)
    {
        // TODO: Ban User with a specific Reson

        await _twitchCache.GetTheTwitchAPI().Helix.Moderation.BanUserAsync(_configuration["Twitch:ChannelId"], _configuration["Twitch:ChannelBotId"], new BanUserRequest { UserId = userId, Reason = reson });
    }
}
