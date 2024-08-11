using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;

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
        SpecialWords? specialWords = _unitOfWork.SpecialWords.FirstOrDefault(s => s.Name.Contains(commandDto.Message) && s.Type == SpecialWordEnum.Count);

        if (specialWords != null) 
        {
            specialWords.TimesUsed++;

            _unitOfWork.SpecialWords.Update(specialWords);
            _unitOfWork.SaveChanges();
        }

        var splitMessage = commandDto.Message.Split(' ');

        if (commandAndResponse != null && commandAndResponse.Active == true)
        {
            if (!_checkAuth.CheckAuth(commandAndResponse.Auth, commandDto.Auth))
            {
                return;
            }
            // Commands with no Logic and simple responce
            if (!commandAndResponse.HasLogic)
            {
                if (_checkAuth.CheckIfCommandAvalibleToUse(commandDto.Message, commandDto.Auth))
                {
                    // TODO: make a Class for this in API.Twitch
                    //_twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), commandAndResponse.Response);
                }
            }
            // Update / Refresh Emotes from 7ttv and Betterttv
            else if (commandAndResponse.Command.Contains("update"))
            {
                // TODO: Request new Emote from 7ttv and Betterttv
            }
            // Comunity day Commands
            // cday,cinfo,cjoin,cleve,cwho,cnext,cremove,cqueue,clast,cstart,cend
            else if (commandAndResponse.Category == CategoryEnum.Queue)
            {
                bool queueIsActive = _unitOfWork.Settings.FirstOrDefault().ComunityDayActive;

                if (queueIsActive || commandDto.Auth.Contains(AuthEnum.Mod) || commandDto.Auth.Contains(AuthEnum.Streamer))
                {
                    _queueCommand.Execute(commandAndResponse, commandDto.Message, commandDto.UserName, commandDto.Origin);
                }
            }
            // modpack, gameinfo, gameprogress
            else if (commandAndResponse.Category == CategoryEnum.Game)
            {
                _gameCommand.Execute(commandAndResponse);
            }
            else if (commandAndResponse.Command.Contains("timer"))
            {
                
            }
            // current time
            else if (commandAndResponse.Command.Contains("time"))
            {
                var localTime = DateTime.Now;

                // Change CEST / UTC + 2 depending if in the summer or winter
                var response = $"{commandAndResponse.Response} + {localTime.Hour}:{localTime.Minute} CEST / UTC + 2";
            }
            // response with the time when stream goes live / with conversion to other time zones
            else if (commandAndResponse.Command.Contains("live"))
            {
                var localTime = DateTime.Now.Date + new TimeSpan(15, 30, 0);
                string response = "undefined";

                if (splitMessage.Length > 1)
                {
                    try
                    {
                        var result = localTime.AddDays(((int)DayOfWeek.Friday - (int)localTime.DayOfWeek + 7) % 7);

                        var timeZone = TimeZoneInfo.ConvertTime(result, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(splitMessage[2]));

                        var offset = (timeZone - result.ToUniversalTime()).Hours;

                        var offsetText = offset > 0 ? $"+{offset}" : $"{offset}";

                        response = $"Stream will be live on {timeZone.DayOfWeek} and {timeZone.AddDays(1).DayOfWeek} at {timeZone.TimeOfDay.ToString()} {splitMessage[2]} / (UTC {offsetText})";
                    }
                    catch (Exception)
                    {
                        response = commandAndResponse.Response;
                    }
                }
                else
                {
                    var result = localTime.AddDays(((int)DayOfWeek.Friday - (int)localTime.DayOfWeek + 7) % 7);

                    response = $"Stream will be live on {result.DayOfWeek} and {result.AddDays(1).DayOfWeek} at {localTime.TimeOfDay.ToString()} {result.Kind} / {result.ToUniversalTime().TimeOfDay} {result.ToUniversalTime().Kind}";
                }

                // TODO: make a Class for this in API.Twitch
                //_twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), $"{commandAndResponse.Response} {reponse}");
            }
            // Fun     flip,random,rainbow,revert,bounce,random,translatehell,gigantify
            else if (commandAndResponse.Category == CategoryEnum.fun)
            {
                //_gameCommand.Execute(commandAndResponse);
            }
            // sstart, sstop, sset
            else if (commandAndResponse.Category == CategoryEnum.Subathon)
            {
                string reponse = $"There is currenty no Subathon going on";

                if (!commandAndResponse.Active)
                {
                    return;
                }
                else if (commandAndResponse.Command.Contains("sstart"))
                {
                    // lgic to start Timer
                }
                else if (commandAndResponse.Command.Contains("sstop"))
                {
                    // lgic to stop Timer
                }
                else if (commandAndResponse.Command.Contains("sset"))
                {
                    // lgic to add time to Timer
                }
            }
            else if (commandAndResponse.Command.Contains("streamstart"))
            {
                // lgic to create DB entry for when the stream has started
            }
            else if (commandAndResponse.Command.Contains("streamstop"))
            {
                // lgic to create DB entry for when the stream has ended
            }
            else if (commandAndResponse.Command.Contains("uptime"))
            {
                // Stream uptime
            }
            else if (commandAndResponse.Command.Contains("clip"))
            {
                // lgic to create a Twitch Clip
            }
            else if (commandAndResponse.Command.Contains("clip2"))
            {
                // lgic to create a OBS Clip / checkpoint
            }
            else if (commandAndResponse.Command.Contains("song"))
            {
                // reads the current song application / API to get the currenty plaing song
            }
            else if (commandAndResponse.Command.Contains("cc"))
            {
                // Get DB enty for Crowd Controll of current game / twitch cattegory
            }
            else if (commandAndResponse.Command.Contains("modpack"))
            {
                // Get DB enty of current game / twitch cattegory
            }
            else if (commandAndResponse.Command.Contains("gameinfo"))
            {
                // Get DB enty of current game / twitch cattegory
            }
            else if (commandAndResponse.Command.Contains("gameprogress"))
            {
                // Get DB enty of current game / twitch cattegory
            }

            else if (commandAndResponse.Command.Contains("so"))
            {
                // 
            }
            else if (commandAndResponse.Command.Contains("lurk"))
            {
                // 
            }
            else if (commandAndResponse.Command.Contains("collab"))
            {
                string response = "";

                if (!commandAndResponse.Active)
                {
                    // No collab
                    response = commandAndResponse.Response;

                    return;
                }

                string peopleLinks = "";

                // get all collab peole from the message
                foreach (var command in splitMessage)
                {
                    // Get DB enty of links for the collab people
                    peopleLinks += "userLink";
                }

                response = $"Current Collab with {peopleLinks}";
            }
            else if (commandAndResponse.Command.Contains("statistics"))
            {
                // shows statistics of the user
            }
            else if (commandAndResponse.Command.Contains("permit"))
            {
                // permit user to post links for 60 seconds (twitch)
            }
            else if (commandAndResponse.Command.Contains("randomuser"))
            {
                // chooses a random user who has chatted in the current stream (Achievements.LastStreamSeen)
            }
        }
        else if (commandAndResponse != null && commandAndResponse.Active == false)
        {
            string reponse = $"{commandAndResponse.Command} is currenty not active";

            // TODO: make a Class for this in API.Twitch
            //_twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), $"the command '{commandAndResponse.Command}' is currently under consturcion 🛠️");
        }
    }
}
