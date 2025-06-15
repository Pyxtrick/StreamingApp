using StreamingApp.API.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.Stream;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageStream : IManageStream
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ITwitchSendRequest _twitchSendRequest;

    private readonly IYouTubeSendRequest _youTubeSendRequest;

    private readonly ITwitchCallCache _twitchCallCache;

    private readonly IManageFile _manageFile;

    public ManageStream(UnitOfWorkContext unitOfWork, ITwitchSendRequest twitchSendRequest, IYouTubeSendRequest youTubeSendRequest, ITwitchCallCache twitchCallCache, IManageFile manageFile)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = twitchSendRequest;
        _youTubeSendRequest = youTubeSendRequest;
        _twitchCallCache = twitchCallCache;
        _manageFile = manageFile;
    }

    public async Task Execute(MessageDto messageDto)
    {
        // removed first word of string
        var noCommandTextMessage = messageDto.Message.Split().Length > 1 ? messageDto.Message[(messageDto.Message.Split()[0].Length + 1)..] : "";

        var splitMessage = messageDto.Message.Split(' ');

        var splitNoCommandTextMessage = noCommandTextMessage.Split(',');

        // TODO: Add Auth Check to the other Get CommandAndResponse to
        CommandAndResponse? commandAndResponse = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Command.Contains(splitMessage[0]) && t.Active);

        if (splitMessage[0].Equals("!clip"))
        {
            // TODO: Twitch clip Stream for the last x seconds
            //_twitchSendRequest.CreateClip(noCommandTextMessage)
        }
        // Limit Commands to Mods and Streamer
        else if (messageDto.Auth.Min() <= AuthEnum.Mod)
        {
            if (splitMessage[0].Equals("!startStream"))
            {
                await StartStream();
            }
            else if (splitMessage[0].Equals("!stopStream"))
            {
                await EndStream();
            }
            else if (splitMessage[0].Equals("!updateCategory"))
            {
                await ChangeCategory();
            }
            else if (splitMessage[0].Equals("!updatetitle"))
            {
                // TODO: Update Twitch Stream Title
                //_twitchSendRequest.UpdateStreamTitle(splitNoCommandTextMessage);
            }
            else if (splitMessage[0].Equals("!so"))
            {
                // TODO: Twitch Shouout user
                if (splitMessage[1] != null)
                {
                    if(messageDto.ChatOrigin == ChatOriginEnum.Youtube)
                    {
                        //_youtubeSendRequest.SendChatMessage($"");
                        return;
                    }
                    var userDetail = _unitOfWork.UserDetail.FirstOrDefault(u => u.UserName == splitMessage[1] && u.Origin == OriginEnum.Twitch);

                    ChannelInfo? channelInfo = await _twitchSendRequest.GetChannelInfo(userDetail.ExternalUserId, true);

                    if (channelInfo != null)
                    {
                        string message = commandAndResponse.Response;

                        message = message.Replace("[User]", splitMessage[1]);
                        message = message.Replace("[GameName]", channelInfo.GameName);
                        message = message.Replace("[Url]", $"https://twitch.tv/{splitMessage[1]}");

                        _twitchSendRequest.SendAnnouncement(message);
                    }
                }
            }
            else if (splitMessage[0].Equals("!pole"))
            {
                if (splitNoCommandTextMessage.Length >= 3)
                {
                    await CreatePole(splitNoCommandTextMessage);
                }
                else
                {
                    _twitchSendRequest.SendChatMessage($"pole needs more information (title, option1, option2)");
                }
            }
            else if (splitMessage[0].Equals("!prediction"))
            {
                if (splitNoCommandTextMessage.Length >= 3)
                {
                    await CreatePrediction(splitNoCommandTextMessage);
                }
                else
                {
                    _twitchSendRequest.SendChatMessage($"prediction needs more information (title, option1, option2)");
                }
            }
        }
    }

    /// <summary>
    /// Starts Stream in DB
    /// </summary>
    /// <returns></returns>
    public async Task StartStream(string youtubeId = "")
    {
        _twitchCallCache.ReseetCounts();

        var stream = _unitOfWork.StreamHistory.OrderBy(sh => sh.Id).ToList().Last();

        // For a new Stream
        if (stream == null || stream.StreamEnd != stream.StreamStart)
        {
            // TODO: reactiveate when _manageFile has changed / Fixed
            //_manageFile.CreateFile();

            var channelInfo = await _twitchSendRequest.GetChannelInfo("", true);

            var utcNow = DateTime.UtcNow;

            Domain.Entities.InternalDB.Stream.Stream newStream = new()
            {
                StreamTitle = channelInfo.Title,
                StreamStart = utcNow,
                StreamEnd = utcNow,
                VodUrl = $"https://www.youtube.com/watch?v={youtubeId}"
            };

            await _unitOfWork.StreamHistory.AddAsync(newStream);
            await _unitOfWork.SaveChangesAsync();

            await ChangeCategory(true);

            string message = $"Stream Started with Title '{newStream.StreamTitle}'";

            _twitchSendRequest.SendChatMessage(message);
            _youTubeSendRequest.SendChatMessage(message);

            // TODO: Send Discord Notification
        }
    }

    /// <summary>
    /// Ends Stream in DB
    /// </summary>
    /// <returns></returns>
    public async Task EndStream()
    {
        var stream = _unitOfWork.StreamHistory.OrderBy(sh => sh.Id).ToList().Last();

        // For editing a Stream for ending it
        // TODO: Test for Later
        if (stream.StreamEnd == stream.StreamStart)
        {
            var streamGame = _unitOfWork.StreamGame.OrderBy(sg => sg.StreamId).Last();

            var utcNow = DateTime.UtcNow;

            streamGame.EndDate = utcNow;

            stream.StreamEnd = utcNow;

            await _unitOfWork.SaveChangesAsync();

            string message = $"Stream is Ending, Thank you for watching <3";

            _twitchSendRequest.SendChatMessage(message);
            _youTubeSendRequest.SendChatMessage(message);

        }
    }

    /// <summary>
    /// Get or add Category and asign to Stream in StreamGame
    /// Create new StreamGame and End last StreamGame
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public async Task ChangeCategory(bool isCreateStream = false)
    {
        var channelInfo = await _twitchSendRequest.GetChannelInfo("", true);

        var stream = _unitOfWork.StreamHistory.OrderBy(sh => sh.StreamStart).Last();
        var gameInfo = _unitOfWork.GameInfo.FirstOrDefault(gi => gi.Game.Equals(channelInfo.GameName));
        var streamGame = _unitOfWork.StreamGame.OrderBy(sh => sh.StreamGameId).Last();

        if (gameInfo == null)
        {
            gameInfo = new GameInfo()
            {
                Game = channelInfo.GameName,
                GameId = channelInfo.GameId,
                Message = "",
                GameCategory = GameCategoryEnum.Info,
            };

            var gameProgress = new GameInfo()
            {
                Game = channelInfo.GameName,
                GameId = channelInfo.GameId,
                Message = "Just Started",
                GameCategory = GameCategoryEnum.Progress,
            };

            await _unitOfWork.GameInfo.AddAsync(gameInfo);
            await _unitOfWork.GameInfo.AddAsync(gameProgress);
        }

        // TODO: Send category change to Twitch
        //if(User.auth == Streamer)
        // TODO: Send info to obs to make a breakpoint if the user is Admin / Streamer

        DateTime utcNow = DateTime.UtcNow;

        StreamGame newStreamGame = new()
        {
            GameCategory = gameInfo,
            Stream = stream,
            StartDate = utcNow,
            EndDate = utcNow
        };

        // For ending last StreamGame
        if (streamGame != null && streamGame.EndDate == streamGame.StartDate)
        {
            streamGame.EndDate = DateTime.UtcNow;
        }

        if (isCreateStream == false)
        {
            _twitchSendRequest.SendChatMessage($"Stream Category has been Changed to '{gameInfo.Game}'");
        }

        await _unitOfWork.StreamGame.AddAsync(newStreamGame);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreatePole(string[] splitMessage)
    {
        // lgic to create a pole with multiple options

        var title = splitMessage[0];
        var time = splitMessage[1];
        List<string> options = [];

        for (int i = 2; i < splitMessage.Length; i++)
        {
            options.Add(splitMessage[i]);
        }

        // TODO: send to twich, Other Platform and (youtube)
    }

    public async Task CreatePrediction(string[] splitMessage)
    {
        // lgic to create prediction with multiple options

        var title = splitMessage[0];
        var time = splitMessage[1];
        List<string> options = [];

        for (int i = 2; i < splitMessage.Length; i++)
        {
            options.Add(splitMessage[i]);
        }

        // TODO: send to twich
    }
}
