using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Stream;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Test.TestBuilder.DB;

public static class GameInfoBuilder
{
    public static GameInfo Create(this UnitOfWorkContext unitOfWork)
    {
        GameInfo gameInfo = new GameInfo();
        unitOfWork.Add(gameInfo);

        return gameInfo;
    }

    public static GameInfo WithDefaults(this GameInfo gameInfo, int id = 1)
    {
        gameInfo.Id = id;

        return gameInfo;
    }

    public static GameInfo WithData(this GameInfo gameInfo, string game, string gameId, string message, GameCategoryEnum gameCategoryEnum)
    {
        gameInfo.Game = game;
        gameInfo.GameId = gameId;
        gameInfo.Message = message;
        gameInfo.GameCategory = gameCategoryEnum;

        return gameInfo;
    }

    public static GameInfo WithDefaults(this GameInfo gameInfo, List<StreamGame> streamGames)
    {
        gameInfo.GameCategories = streamGames;

        return gameInfo;
    }
}
