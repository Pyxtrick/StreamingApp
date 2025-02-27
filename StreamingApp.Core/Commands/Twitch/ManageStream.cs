using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Stream;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageStream : IManageStream
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ISendRequest _twitchSendRequest;

    public ManageStream(UnitOfWorkContext unitOfWork, ISendRequest sendRequest)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = sendRequest;
    }

    public async Task Execute(MessageDto messageDto)
    {
        var channelInfo = await _twitchSendRequest.GetChannelInfo("");

        // removed first word of string
        var noCommandTextMessage = messageDto.Message[(messageDto.Message.Split()[0].Length + 1)..];

        var splitMessage = messageDto.Message.Split(' ');

        var splitNoCommandTextMessage = noCommandTextMessage.Split(' ');

        if (splitMessage[0].Equals("!startStream") || splitMessage[0].Equals("!stopStream"))
        {
            await StartOrEndStream(channelInfo.Title, channelInfo.GameName);
        }
        else if (splitMessage[0].Equals("!updategame"))
        {
            // TODO: Update Twitch Stream Game
            var category = messageDto.Message.Replace("!updategame", "");
            await ChangeCategory(category);
        }
        else if (splitMessage[0].Equals("!updatetitle"))
        {
            // TODO: Update Twitch Stream Title
            //UpdateStreamTitle(splitNoCommandTextMessage);
        }
        else if (splitMessage[0].Equals("!so"))
        {
            // TODO: Twitch Shouout user
            _twitchSendRequest.SendAnnouncement(noCommandTextMessage);
        }
        else if(splitMessage[0].Equals("!clip"))
        {
            // TODO: Twitch clip Stream for the last x seconds
            //CreateClip(splitNoCommandTextMessage)
        }
        else if(splitMessage[0].Equals("!pole"))
        {
            await CreatePole(splitNoCommandTextMessage);
        }
        else if(splitMessage[0].Equals("!prediction"))
        {
            await CreatePrediction(splitNoCommandTextMessage);
        }
    }

    /// <summary>
    /// Starts or Ends Stream in DB
    /// add DB entry or ads an end Date
    /// </summary>
    /// <param name="streamTitle"></param>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public async Task StartOrEndStream(string streamTitle, string categoryName)
    {
        var stream = _unitOfWork.StreamHistory.OrderBy(sh => sh.StreamStart).Last();

        // For a new Stream
        if (stream == null || stream.StreamEnd != stream.StreamStart)
        {
            Domain.Entities.Internal.Stream.Stream newStream = new()
            {
                StreamTitle = streamTitle,
                StreamStart = DateTime.UtcNow,
                StreamEnd = DateTime.UtcNow,
            };

            await _unitOfWork.StreamHistory.AddAsync(newStream);
            await _unitOfWork.SaveChangesAsync();

            //if(User.auth == Streamer)
            // TODO: Send info to obs to start stream and recording if the user is Admin / Streamer

            await ChangeCategory(categoryName);
        }
        // For editing a Stream for ending it
        else if (stream.StreamEnd == stream.StreamStart)
        {
            var streamGame = _unitOfWork.StreamGame.OrderBy(sg => sg.StreamId).Last();
            streamGame.EndDate = DateTime.UtcNow;

            stream.StreamEnd = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            //if(User.auth == Streamer)
            // TODO: Send info to obs to end stream and recording if the user is Admin / Streamer
        }
    }

    /// <summary>
    /// Get or add Category and asign to Stream in StreamGame
    /// Create new StreamGame and End last StreamGame
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public async Task ChangeCategory(string categoryName)
    {
        var stream = _unitOfWork.StreamHistory.OrderBy(sh => sh.StreamStart).Last();
        var gameInfo = _unitOfWork.GameInfo.FirstOrDefault(gi => gi.Game.Equals(categoryName));
        var streamGame = _unitOfWork.StreamGame.OrderBy(sh => sh.StreamGameId).Last();

        if (gameInfo == null)
        {
            gameInfo = new GameInfo()
            {
                Game = categoryName,
                GameId = categoryName,
            };

            await _unitOfWork.GameInfo.AddAsync(gameInfo);
        }

        // TODO: Send category change to Twitch
        //if(User.auth == Streamer)
        // TODO: Send info to obs to make a breakpoint if the user is Admin / Streamer

        DateTime timeNow = DateTime.UtcNow;

        StreamGame newStreamGame = new()
        {
            GameCategory = gameInfo,
            Stream = stream,
            StartDate = timeNow,
            EndDate = timeNow
        };

        // For ending last StreamGame
        if (streamGame != null && streamGame.EndDate == streamGame.StartDate)
        {
            streamGame.EndDate = DateTime.UtcNow;
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
