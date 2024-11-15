using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Logic;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Stream;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageCommands : IManageCommands
{
    private readonly UnitOfWorkContext _unitOfWork;
    private readonly ICheck _checkAuth;
    private readonly IQueueCommand _queueCommand;
    private readonly IGameCommand _gameCommand;
    // TODO: private readonly Func<string, ISendRequest> _sendRequest;
    private readonly ISendRequest _sendRequest;

    //private readonly ISaveDataToFiles _saveDataToFiles;

    public ManageCommands(UnitOfWorkContext unitOfWork, ICheck checkAuth, IQueueCommand queueCommand, IGameCommand gameCommand, ISendRequest sendRequest)
    {
        _unitOfWork = unitOfWork;
        _checkAuth = checkAuth;
        _queueCommand = queueCommand;
        _gameCommand = gameCommand;
        _sendRequest = sendRequest;
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
            else if (commandAndResponse.Command.Contains("pole"))
            {
                // lgic to create a pole with multiple options

                var title = splitMessage[1];
                var time = splitMessage[2];
                List<string> options = [];

                for (int i = 3; i < splitMessage.Length; i++)
                {
                    options.Add(splitMessage[i]);
                }

                // TODO: send to twich, Other Platform and (youtube)
            }
            else if (commandAndResponse.Command.Contains("prediction"))
            {
                // lgic to create prediction with multiple options

                var title = splitMessage[1];
                var time = splitMessage[2];
                List<string> options = [];

                for (int i = 3; i < splitMessage.Length; i++)
                {
                    options.Add(splitMessage[i]);
                }

                // TODO: send to twich
            }
            else if (commandAndResponse.Command.Contains("streamstart"))
            {
                // TODO: Get title and category
                //var chanelInfo = await _sendRequest("TwitchSendRequest").GetChannelInfo();
                var chanelInfo = await _sendRequest.GetChannelInfo();
                if (chanelInfo != null)
                {
                    var title = chanelInfo.Title;
                    var category = chanelInfo.GameName;
                    var game = _unitOfWork.GameInfo.FirstOrDefault(g => g.Game == category && g.GameCategory == GameCategoryEnum.Info);

                    if (game == null)
                    {
                        game = new GameInfo()
                        {
                            Game = category,
                            GameId = category,
                            Message = $"needs Message {category}",
                            GameCategory = GameCategoryEnum.Info,
                        };
                    }

                    var gs = new StreamGame()
                    {
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now, // needst to be changed at the end of category / stream
                        GameCategory = game
                    };

                    var streamHistory = new Domain.Entities.Internal.Stream.Stream()
                    {
                        StreamTitle = title,
                        StreamStart = DateTime.Now,
                        StreamEnd = null,
                        GameCategories = new List<StreamGame> { gs }
                    };

                    _unitOfWork.Add(streamHistory);
                    _unitOfWork.SaveChanges();

                    //_saveDataToFiles.CreateFile(FileNamesEnum.Achievements.ToString());
                }
                else
                {
                    Console.WriteLine("streamstart", "error getting channel Info from Twtich");
                }
            }
            else if (commandAndResponse.Command.Contains("streamstop"))
            {
                // get newest element
                var stream = _unitOfWork.StreamHistory.Include(t => t.GameCategories).ThenInclude(g => g.GameCategory).ToList().Last();
                
                stream.StreamEnd = DateTime.Now;
                stream.GameCategories.Last().EndDate = DateTime.Now;

                _unitOfWork.Update(stream);
                _unitOfWork.SaveChanges();
            }
            else if (commandAndResponse.Command.Contains("updategame"))
            {
                // update twitch category
                
                if (splitMessage[2].Any() && splitMessage[2].Contains("true"))
                {
                    // TODO: Send (youtube) category change
                    //bool success = _sendRequest("TwitchSendRequest").SetChannelInfo(splitMessage[1], null);
                    bool success = _sendRequest.SetChannelInfo(splitMessage[1], null);

                    if (!success)
                    {
                        Console.WriteLine("error sending setting cannel info Category");
                    }
                }

                //var chanelInfo = await _sendRequest("TwitchSendRequest").GetChannelInfo();
                var chanelInfo = await _sendRequest.GetChannelInfo();
                if (chanelInfo != null) {
                    var stream = _unitOfWork.StreamHistory.Include(t => t.GameCategories).ThenInclude(g => g.GameCategory).ToList().Last();
                    var game = _unitOfWork.GameInfo.FirstOrDefault(g => g.Game == chanelInfo.GameName && g.GameCategory == GameCategoryEnum.Info);

                    if (game == null)
                    {
                        game = new GameInfo()
                        {
                            Game = chanelInfo.GameName,
                            GameId = chanelInfo.GameId,
                            Message = $"needs Message {chanelInfo.GameName}",
                            GameCategory = GameCategoryEnum.Info,
                        };
                    }

                    var gs = new StreamGame()
                    {
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        GameCategory = game
                    };

                    stream.GameCategories.Last().EndDate = DateTime.Now;
                    stream.GameCategories.Add(gs);

                    _unitOfWork.Update(stream);
                    _unitOfWork.SaveChanges();

                    //_sendRequest("TwitchSendRequest").SendChatMessage($"Category has been updated to: {game.Game}");
                    _sendRequest.SendChatMessage($"Category has been updated to: {game.Game}");
                }
            }
            else if (commandAndResponse.Command.Contains("updatetitle"))
            {
                //bool success = _sendRequest("TwitchSendRequest").SetChannelInfo(null, splitMessage[1]);
                bool success = _sendRequest.SetChannelInfo(null, splitMessage[1]);

                if (success)
                {
                    //_sendRequest("TwitchSendRequest").SendChatMessage($"Title has been updated to: {splitMessage[1]}");
                    _sendRequest.SendChatMessage($"Title has been updated to: {splitMessage[1]}");
                    return;
                }

                Console.WriteLine("error sending setting cannel info Title");

                // update Stream title on twitch and (Youtube)
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

            //_sendRequest("TwitchSendRequest").SendChatMessage(reponse);
            _sendRequest.SendChatMessage(reponse);

            // TODO: make a Class for this in API.Twitch
            //_twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), $"the command '{commandAndResponse.Command}' is currently under consturcion 🛠️");
        }
    }
}
