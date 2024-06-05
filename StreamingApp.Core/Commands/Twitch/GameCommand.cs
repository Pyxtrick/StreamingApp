using Microsoft.Extensions.Configuration;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;

namespace StreamingApp.Core.Commands.Twitch;

public class GameCommand : IGameCommand
{
    private readonly ITwitchCache _twitchCache;
    private readonly IConfiguration _configuration;

    public GameCommand(ITwitchCache twitchCache, IConfiguration configuration)
    {
        _twitchCache = twitchCache;
        _configuration = configuration;
    }

    // TODO: Read from DB
    List<GameInfo> GameInfoList = new List<GameInfo>
    {
        new GameInfo() { Id = 1, Game = "Just Chatting", GameId = "Just Chatting", Message = "", GameCategory = GameCategoryEnum.Info },
        new GameInfo() { Id = 2, Game = "Just Chatting", GameId = "Just Chatting", Message = "", GameCategory = GameCategoryEnum.ModPack },
        new GameInfo() { Id = 3, Game = "Just Chatting", GameId = "Just Chatting", Message = "", GameCategory = GameCategoryEnum.Server },
        new GameInfo() { Id = 4, Game = "Just Chatting", GameId = "Just Chatting", Message = "", GameCategory = GameCategoryEnum.Progress },

        new GameInfo() { Id = 5, Game = "Lethal Company", GameId = "Lethal Company", Message = "Make Quota and be a gread Asset", GameCategory = GameCategoryEnum.Info },
        new GameInfo() { Id = 6, Game = "Lethal Company", GameId = "Lethal Company", Message = "Modpack Code: 018d26ca-ecd1-661a-a3ee-3a7afeef1098", GameCategory = GameCategoryEnum.ModPack },
        new GameInfo() { Id = 7, Game = "Lethal Company", GameId = "Lethal Company", Message = "Server Located at: XXXX", GameCategory = GameCategoryEnum.Server },
        new GameInfo() { Id = 8, Game = "Lethal Company", GameId = "Lethal Company", Message = "There is no Progress in this Game and now go Out and make Quota", GameCategory = GameCategoryEnum.Progress },

        new GameInfo() { Id = 5, Game = "Palworld", GameId = "Palworld", Message = "Get your Pals Mate", GameCategory = GameCategoryEnum.Info },
        new GameInfo() { Id = 6, Game = "Palworld", GameId = "Palworld", Message = "There is no modpack used in this palythroue", GameCategory = GameCategoryEnum.ModPack },
        new GameInfo() { Id = 7, Game = "Palworld", GameId = "Palworld", Message = "Server Located at: XXXX", GameCategory = GameCategoryEnum.Server },
        new GameInfo() { Id = 8, Game = "Palworld", GameId = "Palworld", Message = "2 Towers beeten Level 40+", GameCategory = GameCategoryEnum.Progress },
    };

    public async void Execute(CommandAndResponse commandAndResponse)
    {
        GetChannelInformationResponse channelInfo = await _twitchCache.GetTheTwitchAPI().Helix.Channels.GetChannelInformationAsync(_configuration["Twitch:ClientId"]);

        string gameId = channelInfo.Data[0].GameId;
        string gameName = channelInfo.Data[0].GameName;

        string responseMessage = "";

        switch (commandAndResponse.Command)
        {
            case "gameinfo":
                var gameInfo = GameInfoList.FirstOrDefault(t => t.Game == gameName && t.GameCategory == GameCategoryEnum.Info);

                if (gameInfo != null)
                {
                    responseMessage = gameInfo.Message;
                }
                else
                {
                    // There is no Info about this Category: Game
                    responseMessage = $"{commandAndResponse.Response}{gameName}";
                }
                break;
            case "modpack":
                var gameModpack = GameInfoList.FirstOrDefault(t => t.Game == gameName && t.GameCategory == GameCategoryEnum.ModPack);

                if (gameModpack != null)
                {
                    responseMessage = gameModpack.Message;
                }
                else
                {
                    // There is no Modpack about this Category: Game
                    responseMessage = $"{commandAndResponse.Response}{gameName}";
                }
                break;
            case "gameprogress":
                var gameProgress = GameInfoList.FirstOrDefault(t => t.Game == gameName && t.GameCategory == GameCategoryEnum.Progress);

                if (gameProgress != null)
                {
                    responseMessage = gameProgress.Message;
                }
                else
                {
                    // There is no Progress about this Category: Game
                    responseMessage = $"{commandAndResponse.Response}{gameName}";
                }
                break;
        }
        _twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), $"{responseMessage}");
    }
}
