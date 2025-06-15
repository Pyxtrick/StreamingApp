using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class GameCommand : IGameCommand
{
    private readonly ITwitchSendRequest _sendRequest;

    private readonly UnitOfWorkContext _unitOfWork;

    public GameCommand(ITwitchSendRequest sendRequest, UnitOfWorkContext unitOfWork)
    {
        _sendRequest = sendRequest;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(CommandAndResponse commandAndResponse)
    {
        // Change to Gameinfo from ongoing Stream 
        ChannelInfo channelInfo = new();

        var stream = _unitOfWork.StreamGame.Include("GameCategory").OrderBy(sh => sh.StartDate).ToList().Last();

        try
        {
            var ci = await _sendRequest.GetChannelInfo(null, true);

            if (ci != null)
            {
                channelInfo = ci;
            }
            else
            {
                channelInfo.GameName = stream.GameCategory.Game;
                channelInfo.GameId = stream.GameCategory.GameId;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{DateTime.Now} Error with GetChannelInfo");

            channelInfo.GameName = stream.GameCategory.Game;
            channelInfo.GameId = stream.GameCategory.GameId;
        }

        string gameId = channelInfo.GameId;
        string gameName = channelInfo.GameName;

        string responseMessage = "";

        switch (commandAndResponse.Command)
        {
            //Fix
            case "gameinfo":
                var gameInfo = _unitOfWork.GameInfo.FirstOrDefault(t => t.GameId == gameId && t.GameCategory == GameCategoryEnum.Info);

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
                var gameModpack = _unitOfWork.GameInfo.FirstOrDefault(t => t.GameId == gameId && t.GameCategory == GameCategoryEnum.ModPack);

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
                var gameProgress = _unitOfWork.GameInfo.FirstOrDefault(t => t.GameId == gameId && t.GameCategory == GameCategoryEnum.Progress);

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

        _sendRequest.SendChatMessage(responseMessage);
    }
}
