using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;
using System;
using TwitchLib.Api.Helix.Models.Clips.GetClips;
using TwitchLib.Api.Helix.Models.Soundtrack;

namespace StreamingApp.Core.Commands;

public class AddDBData : IAddDBData
{
    private readonly UnitOfWorkContext _unitOfWork;

    public AddDBData(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var t = _unitOfWork.CommandAndResponse.ToList();

        if (t.Count() == 0)
        {
            List<CommandAndResponse> commandsStaticResponse = new List<CommandAndResponse>
            {
                new CommandAndResponse() {Command = "about", Response = "Pyxtrick is a stramer From Switzerland who loves Gaming and Programming", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "links", Response = "All links are found here: https://pyxtrick.carrd.co/", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "lurk", Response = "XXX coes udercover. Have fun Lurking ", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },
                new CommandAndResponse() {Command = "socials", Response = "All my Socials https://x.com/Pyxtrick | https://www.youtube.com/@Pyxtrick", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "discord", Response = "Discord is currenty under constuction 🛠️", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "donate", Response = "If you want to support the stream, you can donate using the following link: XYZ", Description = "", Active = false, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "collab", Response = "There is currenty no Collab going on", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },
                
                new CommandAndResponse() {Command = "raidsub", Response = "There is currenty no Twitch Raid message with Sub Emotes use raid command instead", Description = "", Active = false, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "raidbit", Response = "There is currenty no Twitch Raid message with Bit Emotes use raid command instead", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "raid", Response = "<3 Pyxtrick twitchRaid", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                
                new CommandAndResponse() {Command = "emotes", Response = "Cannot see Emotes like hiii get 7tv browser addon from https://7tv.app/", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "statistics", Response = "Currenty no statistics can be found :(", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },
                
                new CommandAndResponse() {Command = "language", Response = "English only please | bitte nur englisch | solo inglés por favor | solo inglese per favore | apenas inglês por favor | anglais seulement s'il vous plait | alleen engels alstublieft | proszę tylko po angielsku | только английский пожалуйста | 英語のみお願いします | 请只说英语 | 영어만 쓰시길 바랍니다", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "english", Response = "The Stream is Primarly Englisch only that includes the chat.", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "english2", Response = "The Stream is Primarly Englisch only that includes the chat. But in rare acations it can be in Schwizer Dütsch.", Description = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Undefined, HasLogic = false },

                new CommandAndResponse() {Command = "streamtime", Response = "next stream will be on Friday or Saturday", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "uptime", Response = "Online since HH:MM:SS DD.MM.YYYY Live for XD Xh Xm Xs", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },
                new CommandAndResponse() {Command = "updategame", Response = "Stream Game has been updated to: ", Description = "updategame [game]", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.streamupdate, HasLogic = true },
                new CommandAndResponse() {Command = "updatetitle", Response = "Stream Title has been updated to: ", Description = "updatetitle [title]", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.streamupdate, HasLogic = true },

                // Adds timer for x time
                new CommandAndResponse() {Command = "timer", Response = "Timer has been set for X minutes", Description = "timer [minutes]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },

                // in CEST / UTC
                new CommandAndResponse() {Command = "time", Response = "current time is: ", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },
                
                // TOOD: change time when time zone is used (live est)
                // TODO: Dynamicly change time on time change CEST / CET
                new CommandAndResponse() {Command = "live", Response = "Use the TZ identifier for this command !live https://en.wikipedia.org/wiki/List_of_tz_database_time_zones", Description = "live [TZ]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },

                // TODO: so @fufu gets link from db and shows them with text
                // Soudout to a User / Twitch Channel
                new CommandAndResponse() {Command = "so", Response = "", Description = "so [user1]", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Undefined, HasLogic = true },
                new CommandAndResponse() {Command = "pole", Response = "", Description = "pole [title] [time] [option1] [option2] [option3]", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Undefined, HasLogic = true },
                
                // TODO: have an immage for showing all command and Platformn ID's for
                // For steam join the Group XXX | EA: XXX | Battlenet: XXX | XBOX for PC: XXX
                // If you are in Discord join the waiting room so we can pull you wen it is your turn
                new CommandAndResponse() {Command = "cday", Response = "if you want to join the Queue use !cjoin", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cinfo", Response = "if you want to join the Queue use !cjoin", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cjoin", Response = "Has have Joined the Queue on Possition", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cleve", Response = "Has have Left the Queue", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cwho", Response = "Current users are: ", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cnext", Response = "Next users are: ", Description = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cremove", Response = "Users have been Removed from the Active Queue", Description = "cremove [user]", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cqueue", Response = "You are in the Active Queue", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "clast", Response = "Has Been moved to last place in Queue", Description = "clast [user]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "crandom", Response = "Has Been Choosen", Description = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "ccount", Response = "There are currenty X users in", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cstart", Response = "Queue is now open to join", Description = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue, HasLogic = true },
                new CommandAndResponse() {Command = "cend", Response = "Queue is closed and no one is able to join", Description = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue, HasLogic = true },

                new CommandAndResponse() {Command = "rules", Response = "", Description = "", Active = false, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },
                new CommandAndResponse() {Command = "vote", Response = "If you want to vote for me Here is the Link: ", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },

                new CommandAndResponse() {Command = "cc", Response = "Crowd Controll Links:", Description = "", Active = false, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },
                new CommandAndResponse() {Command = "modpack", Response = "There is no Modpack about this Category: ", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Game, HasLogic = true },
                new CommandAndResponse() {Command = "gameinfo", Response = "There is no Info about this Category: ", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Game, HasLogic = true },
                new CommandAndResponse() {Command = "gameprogress", Response = "There is no Progress about this Category: ", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Game, HasLogic = true },
                
                new CommandAndResponse() {Command = "song", Response = "There is no Info about this Song", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Song, HasLogic = true },
                
                // Subathon
                new CommandAndResponse() {Command = "sstart", Response = "Subathon timer has been started", Description = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Subathon, HasLogic = true },
                new CommandAndResponse() {Command = "sstop", Response = "Subathon timer has been stoped", Description = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Subathon, HasLogic = true },
                new CommandAndResponse() {Command = "sset", Response = "Subathon timer has been set to: ", Description = "sset [time(HH:MM:SS)]", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Subathon, HasLogic = true },

                new CommandAndResponse() {Command = "streamstart", Response = "defines the start time of the stream in the db", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },
                new CommandAndResponse() {Command = "streamstop", Response = "defines the end time of the stream in the db", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },

                // creates a twitch clip
                new CommandAndResponse() {Command = "clip", Response = "clips the last X Seconds", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },
                new CommandAndResponse() {Command = "clip2", Response = "Creates a time stamp of the current recording", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = true },

                //TODO: Fun chat things
                new CommandAndResponse() {Command = "fun", Response = "to use fun chat messages use one of the fallowing commands with the message: flip, random, rainbow, revert, bounce, random, translatehell, gigantify", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun, HasLogic = true },
                // flips chat message around
                new CommandAndResponse() {Command = "flip", Response = "", Description = "flip [degree] [message]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun, HasLogic = true },
                // displayes the text in a random order
                new CommandAndResponse() {Command = "random", Response = "", Description = "random [message]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun, HasLogic = true },
                // changes the color of the text
                new CommandAndResponse() {Command = "rainbow", Response = "", Description = "rainbow [message]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun, HasLogic = true },
                // reverts the text from back to front (front to back from text the reverts)
                new CommandAndResponse() {Command = "revert", Response = "", Description = "revert [message]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun, HasLogic = true },
                // makes the text bounce
                new CommandAndResponse() {Command = "bounce", Response = "", Description = "bounce [message]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun, HasLogic = true },
                // translates the text multiple times
                new CommandAndResponse() {Command = "translatehell", Response = "", Description = "translatehell [message]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun, HasLogic = true },
                // changes the size of the text and emotes
                new CommandAndResponse() {Command = "gigantify", Response = "", Description = "gigantify [message]", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun, HasLogic = true },

                new CommandAndResponse() {Command = "clipper", Response = "Send some love to all the Clipper's ", Description = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined, HasLogic = false },

                new CommandAndResponse() {Command = "update", Response = "Chat Emotes have been updated", Description = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Undefined, HasLogic = true },
            };
            _unitOfWork.CommandAndResponse.AddRange(commandsStaticResponse);

            await _unitOfWork.SaveChangesAsync();
        }

        var s = _unitOfWork.SpecialWords.ToList();

        if (s.Count() == 0)
        {
            List<SpecialWords> bannedWords = new List<SpecialWords>
            {
                new SpecialWords() { Name = "https", Comment = "url", Type = SpecialWordEnum.Delete },
                new SpecialWords() { Name = "http", Comment = "url", Type = SpecialWordEnum.Delete },
                new SpecialWords() { Name = "Best and Cheap Viewers on", Comment = "ban", Type = SpecialWordEnum.Banned },

                new SpecialWords() { Name = "https://7tv.app", Comment = "url", Type = SpecialWordEnum.Allowed },

                // https://cdn.discordapp.com/attachments/1192605544008134666/1214569815705124874/
                // check if the two numbers at the end is the discord sserver and chanel
                new SpecialWords() { Name = "https://cdn.discordapp.com", Comment = "url", Type = SpecialWordEnum.Allowed },
            };

            List<SpecialWords> SpecialWords = new List<SpecialWords>
            {
                new SpecialWords() { Id = 1, Name = "+1", Comment = "counts every death", TimesUsed = 0, Type = SpecialWordEnum.Count },
                new SpecialWords() { Id = 1, Name = "pipefalling", Comment = "Allert Lets Pipe Fall", TimesUsed = 0, Type = SpecialWordEnum.Count },
            };
            _unitOfWork.SpecialWords.AddRange(bannedWords);

            await _unitOfWork.SaveChangesAsync();
        }


        var g = _unitOfWork.GameInfo.ToList();

        if (g.Count() == 0)
        {
            List<GameInfo> gameInfoList = new List<GameInfo>
            {
                new GameInfo() { Game = "Just Chatting", GameId = "Just Chatting", Message = "", GameCategory = GameCategoryEnum.Info },
                new GameInfo() { Game = "Just Chatting", GameId = "Just Chatting", Message = "", GameCategory = GameCategoryEnum.ModPack },
                new GameInfo() { Game = "Just Chatting", GameId = "Just Chatting", Message = "", GameCategory = GameCategoryEnum.Server },
                new GameInfo() { Game = "Just Chatting", GameId = "Just Chatting", Message = "", GameCategory = GameCategoryEnum.Progress },

                new GameInfo() { Game = "Lethal Company", GameId = "Lethal Company", Message = "Make Quota and be a gread Asset", GameCategory = GameCategoryEnum.Info },
                new GameInfo() { Game = "Lethal Company", GameId = "Lethal Company", Message = "Modpack Code: 018d26ca-ecd1-661a-a3ee-3a7afeef1098", GameCategory = GameCategoryEnum.ModPack },
                new GameInfo() { Game = "Lethal Company", GameId = "Lethal Company", Message = "Server Located at: XXXX", GameCategory = GameCategoryEnum.Server },
                new GameInfo() { Game = "Lethal Company", GameId = "Lethal Company", Message = "There is no Progress in this Game and now go Out and make Quota", GameCategory = GameCategoryEnum.Progress },

                new GameInfo() { Game = "Palworld", GameId = "Palworld", Message = "Get your Pals Mate", GameCategory = GameCategoryEnum.Info },
                new GameInfo() { Game = "Palworld", GameId = "Palworld", Message = "There is no modpack used in this palythroue", GameCategory = GameCategoryEnum.ModPack },
                new GameInfo() { Game = "Palworld", GameId = "Palworld", Message = "Server Located at: XXXX", GameCategory = GameCategoryEnum.Server },
                new GameInfo() { Game = "Palworld", GameId = "Palworld", Message = "2 Towers beeten Level 40+", GameCategory = GameCategoryEnum.Progress },
            };
            await _unitOfWork.AddRangeAsync(gameInfoList);

            await _unitOfWork.SaveChangesAsync();
        }

        var settings = _unitOfWork.Settings.ToList();

        if (settings.Count() == 0)
        {
            List<Settings> settingsList = new List<Settings>
            {
                new Settings() { Origin = ChatOriginEnum.Undefined, AllChat = AuthEnum.undefined, MuteAllerts = false, ComunityDayActive = false, Delay = "*/10 * * * *", AllertDelayS = 2, TimeOutSeconds = 60, SpamAmmount = 5}
            };

            await _unitOfWork.AddRangeAsync(settingsList);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
