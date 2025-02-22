using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal.Stream;
using StreamingApp.Domain.Entities.Internal.User;
using TwitchLib.Api.Auth;

namespace StreamingApp.Core.Commands.DB;
public class UpdateStream : IUpdateStream
{
    private readonly UnitOfWorkContext _unitOfWork;

    public UpdateStream(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
}
