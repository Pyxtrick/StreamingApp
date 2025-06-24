using Microsoft.Extensions.Configuration;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB.Settings;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class QueueCommand : IQueueCommand
{
    private readonly ITwitchCache _twitchCache;
    private readonly IQueueCache _queueCache;
    private readonly UnitOfWorkContext _unitOfWork;
    private readonly IConfiguration _configuration;

    public QueueCommand(ITwitchCache twitchCache, IQueueCache queueCache, UnitOfWorkContext unitOfWork, IConfiguration configuration)
    {
        _twitchCache = twitchCache;
        _queueCache = queueCache;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public void Execute(CommandAndResponse commandAndResponse, string message, string userName, OriginEnum origin)
    {
        int ammount = int.Parse(message.Split(" ")[1]);

        Settings settings = _unitOfWork.Settings.First(s => s.Origin == origin);

        switch (commandAndResponse.Command)
        {
            case "cstart": // Starts the queue for every one
                settings.ComunityDayActive = true;

                _unitOfWork.Update(settings);
                _unitOfWork.SaveChanges();

                SendMessage(commandAndResponse.Response, origin);
                break;
            case "cend": // Stops the queue but not removes the data
                settings.ComunityDayActive = true;

                _unitOfWork.Update(settings);
                _unitOfWork.SaveChanges();

                SendMessage(commandAndResponse.Response, origin);
                break;
        }

        switch (commandAndResponse.Command)
        {
            case "cday": // Displays information about the Comunity day
                SendMessage(commandAndResponse.Response, origin);
                break;
            case "cinfo": // Displays information about the Comunity day
                SendMessage(commandAndResponse.Response, origin);
                break;
            case "cjoin": // able to join the queue once
                if (settings.ComunityDayActive)
                {
                    UserQueueDto userQueueDto = new(userName, true, 0, OriginEnum.Twitch);
                    var possition = _queueCache.AddUserToQueue(userQueueDto);
                    // @User Has have Joined the Queue on Possition X
                    // Even if the user has allready joined previously
                    SendMessage($"@{userName} {commandAndResponse.Response} {possition}", origin);
                }
                break;
            case "cleve": // leves the queue but not when the user was already its turn
                var t = _queueCache.RemoveQueueUser(userName);
                if (t)
                {
                    // @User Has have Left the Queue
                    SendMessage($"@{userName} {commandAndResponse.Response}", origin);
                }
                break;
            case "cwho": // shows the current X users in the Queue
                IList<UserQueueDto> currentUserQueue = _queueCache.GetQueueUsers(ammount);

                var queueMessageWho = commandAndResponse.Response;

                foreach (var user in currentUserQueue)
                {
                    queueMessageWho = $"{queueMessageWho} @{user.UserName}";
                }

                // Current users are: @User1 @User2
                SendMessage(queueMessageWho, origin);

                break;
            case "cnext": // shows the next X Users in the queue / moves the queue
                IList<UserQueueDto> newUserQueue = _queueCache.MoveQueue(ammount);

                string queueMessageNext = commandAndResponse.Response;

                foreach (var user in newUserQueue)
                {
                    queueMessageNext = $"{queueMessageNext} @{user.UserName}";
                }

                // Next users are: @User1 @User2
                SendMessage(queueMessageNext, origin);
                break;
            case "cremove": // Removes all users from the queue
                _queueCache.RemoveCurrentQueue(ammount);
                SendMessage(commandAndResponse.Response, origin);
                break;
            case "cqueue":
                int queuePosition = _queueCache.GetQueuePosition(userName);

                if (queuePosition > 0)
                {

                    // @User You are in Queue X
                    message = commandAndResponse.Response.Replace("[User]", userName);
                    message = message.Replace("[Position]", queuePosition.ToString());
                    SendMessage(message, origin);
                }
                else
                {
                    SendMessage($"You are not in the Queue {userName}", origin);
                }
                break;
            case "clast": // Moves the user to last place in the queue
                // TODO: move user XXX to last place
                // TODO: able for a mod to move a user
                var isSuccess = _queueCache.MoveUserQueue(userName);

                if (isSuccess)
                {
                    SendMessage(commandAndResponse.Response, origin);
                }
                break;

            case "crandom":
                // TOOD: Save user to specific location so it will be tracked / higlitet in chat
                var randomUser = _queueCache.ChooseRandomFromQueue();

                if (randomUser != null)
                {
                    SendMessage($"@{randomUser.UserName} {commandAndResponse.Response}", origin);
                }
                break;
            /**case "ccount":
                var count = _queueCache.GetQueueCount();

                if (count != 0)
                {
                    SendMessage($"{commandAndResponse.Response.Replace('X', char.Parse(count.ToString()))}", origin);
                }
                else
                {
                    SendMessage($"There are no Users in the list", origin);
                }
                break;**/
        }
    }

    private void SendMessage(string response, OriginEnum origin)
    {
        switch (origin)
        { 
            case OriginEnum.Twitch:
                _twitchCache.GetOwnerOfChannelConnection().SendMessage(_configuration["Twitch:Channel"], response);
                break;
            case OriginEnum.Youtube:
                break;
        }
    }
}
