using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.InternalDB.Stream;
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

    public async Task<string> Execute(CommandAndResponse commandAndResponse)
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

        string responseMessage = "";

        GameInfo gameInfo = null;

        switch (commandAndResponse.Command)
        {
            //Fix
            case "gameinfo":
                gameInfo = _unitOfWork.GameInfo.FirstOrDefault(t => t.GameId == channelInfo.GameId && t.GameCategory == GameCategoryEnum.Info);
                break;
            case "modpack":
                gameInfo = _unitOfWork.GameInfo.FirstOrDefault(t => t.GameId == channelInfo.GameId && t.GameCategory == GameCategoryEnum.ModPack);
                break;
            case "gameprogress":
                gameInfo = _unitOfWork.GameInfo.FirstOrDefault(t => t.GameId == channelInfo.GameId && t.GameCategory == GameCategoryEnum.Progress);
                break;
        }

        if (gameInfo != null)
        {
            responseMessage = gameInfo.Message;
        }
        else
        {
            // There is no Info about this Category: Game
            responseMessage = $"{commandAndResponse.Response} {channelInfo.GameName}";
        }

        await _sendRequest.SendChatMessage(responseMessage);

        return responseMessage;
    }
}
