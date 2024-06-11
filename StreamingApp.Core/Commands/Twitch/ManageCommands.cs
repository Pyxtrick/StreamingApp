using Google.Apis.Bigquery.v2.Data;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;
using System;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageCommands : IManageCommands
{
    private readonly UnitOfWorkContext _unitOfWork;
    private readonly ICheck _checkAuth;

    private readonly IQueueCommand _queueCommand;
    private readonly IGameCommand _gameCommand;

    public ManageCommands(UnitOfWorkContext unitOfWork, ICheck checkAuth, IQueueCommand queueCommand, IGameCommand gameCommand)
    {
        _unitOfWork = unitOfWork;
        _checkAuth = checkAuth;
        _queueCommand = queueCommand;
        _gameCommand = gameCommand;
    }

    public async Task Execute(CommandDto commandDto)
    {
        CommandAndResponse? commandAndResponse = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Command.Contains(commandDto.Message) && t.Active);

        var splitMessage = commandDto.Message.Split(' ');

        if (commandAndResponse != null && commandAndResponse.Active == true)
        {
            if (!_checkAuth.CheckAuth(commandAndResponse.Auth, commandDto.Auth))
            {
                return;
            }

            if (commandAndResponse.Command.Contains("update"))
            {
                // TODO: Request new Emote from 7ttv and Betterttv
            }
            else if (commandAndResponse.Category == CategoryEnum.Queue)
            {
                bool queueIsActive = _unitOfWork.Settings.FirstOrDefault().ComunityDayActive;

                if (queueIsActive || commandDto.Auth.Contains(AuthEnum.Mod) || commandDto.Auth.Contains(AuthEnum.Streamer))
                {
                    _queueCommand.Execute(commandAndResponse, commandDto.Message, commandDto.UserName, commandDto.Origin);
                }
            }
            else if (commandAndResponse.Category == CategoryEnum.Game)
            {
                _gameCommand.Execute(commandAndResponse);
            }
            else if (commandAndResponse.Command.Contains("live"))
            {
                var localTime = DateTime.Now.Date + new TimeSpan(15, 30, 0);
                string reponse = "undefined";


                if (splitMessage.Length > 1)
                {
                    try
                    {
                        var result = localTime.AddDays(((int)DayOfWeek.Friday - (int)localTime.DayOfWeek + 7) % 7);

                        var timeZone = TimeZoneInfo.ConvertTime(result, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(splitMessage[2]));

                        var offset = (timeZone - result.ToUniversalTime()).Hours;

                        var offsetText = offset > 0 ? $"+{offset}" : $"{offset}";

                        reponse = $"Stream will be live on {timeZone.DayOfWeek} and {timeZone.AddDays(1).DayOfWeek} at {timeZone.TimeOfDay.ToString()} {splitMessage[2]} / (UTC {offsetText})";
                    }
                    catch (Exception)
                    {
                        reponse = commandAndResponse.Response;
                    }
                }
                else
                {
                    var result = localTime.AddDays(((int)DayOfWeek.Friday - (int)localTime.DayOfWeek + 7) % 7);

                    reponse = $"Stream will be live on {result.DayOfWeek} and {result.AddDays(1).DayOfWeek} at {localTime.TimeOfDay.ToString()} {result.Kind} / {result.ToUniversalTime().TimeOfDay} {result.ToUniversalTime().Kind}";
                }

                // TODO: make a Class for this in API.Twitch
                //_twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), $"{commandAndResponse.Response} {reponse}");
            }
            else
            {
                if (_checkAuth.CheckIfCommandAvalibleToUse(commandDto.Message, commandDto.Auth))
                {
                    // TODO: make a Class for this in API.Twitch
                    //_twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), commandAndResponse.Response);
                }
            }
        }
        if (commandAndResponse != null && commandAndResponse.Active == false)
        {
            // TODO: make a Class for this in API.Twitch
            //_twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), $"the command '{commandAndResponse.Command}' is currently under consturcion 🛠️");
        }
    }
}
