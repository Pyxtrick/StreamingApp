using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.Interfaces;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Utility.TextToSpeach;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class ManageCommands : IManageCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ITwitchSendRequest _twitchSendRequest;

    private readonly IGameCommand _gameCommand;

    private readonly IManageTextToSpeach _manageTextToSpeach;

    private readonly IManageCommandsWithLogic _manageCommandsWithLogic;

    private readonly IManageStream _manageStream;

    private readonly IHubContext<ChatHub> _hubContext;

    public ManageCommands(UnitOfWorkContext unitOfWork, ITwitchSendRequest twitchSendRequest, IGameCommand gameCommand, IManageTextToSpeach manageTextToSpeach, IManageCommandsWithLogic manageCommandsWithLogic, IManageStream manageStream, IHubContext<ChatHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = twitchSendRequest;
        _gameCommand = gameCommand;
        _manageTextToSpeach = manageTextToSpeach;
        _manageCommandsWithLogic = manageCommandsWithLogic;
        _manageStream = manageStream;
        _hubContext = hubContext;
    }

    public async Task Execute(MessageDto messageDto)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveCommandMessage", messageDto);

        string commandText = messageDto.Message.Split(' ').ToList()[0].Trim('!');

        CommandAndResponse? commandAndResponse = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Command.Equals(commandText) && t.Active && messageDto.Auth.Min() <= t.Auth);

        if (commandAndResponse != null && commandAndResponse.Active == true && messageDto.Auth.First() <= commandAndResponse.Auth)
        {
            if (commandAndResponse.Category == CategoryEnum.Queue)
            {
                bool queueIsActive = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == messageDto.Origin).ComunityDayActive;

                if (queueIsActive)
                {
                    //_queueCommand.Execute(commandAndResponse, messageDto.Message, messageDto.UserName, messageDto.Origin);
                }

                Console.WriteLine("Command Queue");
            }
            else if (commandAndResponse.Category == CategoryEnum.Game)
            {
                await _gameCommand.Execute(commandAndResponse);
                Console.WriteLine("Command Game");
            }
            // sstart, sstop, sset
            else if (commandAndResponse.Category == CategoryEnum.Subathon)
            {
                //commandAndResponse.Response.Replace("[SubathonTimer]", DateTime.Now.ToString());
                //_subathonCommand.Execute(commandAndResponse);
                Console.WriteLine("Command Subathon");
            }
            // Fun     flip,random,rainbow,revert,bounce,random,translatehell,gigantify
            else if (commandAndResponse.Category == CategoryEnum.Fun)
            {
                //_ManageFun.Execute(commandAndResponse);
                if (messageDto.Message.Contains("say"))
                {
                    _manageTextToSpeach.Execute(messageDto);
                }
            }
            else if (commandAndResponse.HasLogic)
            {
                await _manageCommandsWithLogic.Execute(messageDto, commandAndResponse);
                Console.WriteLine("Command Has Logic");
            }
            else if (commandAndResponse.Category == CategoryEnum.StreamUpdate)
            {
                await _manageStream.Execute(messageDto);
            }
            else
            {
                // TODO: Replace parts in the message
                string message = commandAndResponse.Response;

                var stream = _unitOfWork.StreamHistory.OrderBy(s => s.StreamStart).Last();

                message = message.Replace("[User]", messageDto.UserName);
                message = message.Replace("[Time]", DateTime.Now.ToString());
                message = message.Replace("[StreamStartTime]", stream.StreamStart.ToLocalTime().ToString());
                message = message.Replace("[StreamLiveTime]", DateTime.UtcNow.Subtract(stream.StreamStart).ToString());

                await _twitchSendRequest.SendChatMessage(message);
            }
        }
    }
}
