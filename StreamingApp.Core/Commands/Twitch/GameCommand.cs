using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class GameCommand : IGameCommand
{
    private readonly ITwitchSendRequest _sendRequest;

    private readonly UnitOfWorkContext _unitOfWork;

    public GameCommand(ITwitchSendRequest sendRequest, UnitOfWorkContext unitOfWorkContext)
    {
        _sendRequest = sendRequest;
        _unitOfWork = unitOfWorkContext;
    }

    public async void Execute(CommandAndResponse commandAndResponse)
    {
        var channelInfo = await _sendRequest.GetChannelInfo(null);

        string gameId = channelInfo.GameId;
        string gameName = channelInfo.GameName;

        string responseMessage = "";

        switch (commandAndResponse.Command)
        {
            case "gameinfo":
                var gameInfo = _unitOfWork.GameInfo.FirstOrDefault(t => t.Game == gameName && t.GameCategory == GameCategoryEnum.Info);

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
                var gameModpack = _unitOfWork.GameInfo.FirstOrDefault(t => t.Game == gameName && t.GameCategory == GameCategoryEnum.ModPack);

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
                var gameProgress = _unitOfWork.GameInfo.FirstOrDefault(t => t.Game == gameName && t.GameCategory == GameCategoryEnum.Progress);

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
