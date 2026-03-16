using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Stream;
using Stream = StreamingApp.Domain.Entities.InternalDB.Stream.Stream;

namespace StreamingApp.Test.TestBuilder.DB;

public static class StreamGameBuilder
{
    public static StreamGame Create(this UnitOfWorkContext unitOfWork)
    {
        StreamGame streamGame = new StreamGame();
        unitOfWork.Add(streamGame);

        return streamGame;
    }

    public static StreamGame WithDefaults(this StreamGame streamGame, int id = 1)
    {
        streamGame.StreamGameId = id;
        streamGame.GameCategoryId = id;
        streamGame.StreamId = id;
        streamGame.StartDate = DateTime.UtcNow;
        streamGame.EndDate = DateTime.UtcNow.AddHours(4);

        return streamGame;
    }

    public static StreamGame WithGameCategory(this StreamGame streamGame, GameInfo gameInfo)
    {
        streamGame.GameCategory = gameInfo;

        return streamGame;
    }

    public static StreamGame WithStream(this StreamGame streamGame, Stream stream)
    {
        streamGame.Stream = stream;

        return streamGame;
    }
}
