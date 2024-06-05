using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands;

public class AddDBData : IAddDBData
{
    private readonly UnitOfWorkContext _unitOfWork;
    //private readonly UnitOfWorkContext _unitOfWork;

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
                new CommandAndResponse() {Command = "about", Response = "Pyxtrick is a stramer From Switzerland who loves Gaming and Programming", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "links", Response = "All links are found here: XYZ", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "discord", Response = "Discord is currenty under constuction 🛠️", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "donate", Response = "If you want to support the stream, you can donate using the following link: XYZ", Active = false, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "collab", Response = "There is currenty no Collab going on", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "streamtime", Response = "next stream will be on Friday or Saturday", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "raid1", Response = "There is currenty no Twitch Raid message with Sub Emotes use raid2 command instead", Active = false, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "raid2", Response = "<3 Pyxtrick twitchRaid", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "emotes", Response = "Cannot see Emotes like GIGACHAD get 7tv browser addon from https://7tv.app/", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "statistics", Response = "Currenty no statistics can be found :(", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "language", Response = "The Stream is Primarly Englisch only that includes the chat.", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "language2", Response = "The Stream is Primarly Englisch only that includes the chat. But in rare acations it can be in Schweizer Deutch.", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "uptime", Response = "Online since HH:MM:SS DD.MM.YYYY Live for XD Xh Xm Xs", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },

                new CommandAndResponse() {Command = "updategame", Response = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.streamupdate },
                new CommandAndResponse() {Command = "updatetitle", Response = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.streamupdate },

                new CommandAndResponse() {Command = "lurk", Response = "XXX coes udercover. Have fun Lurking ", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "time", Response = "current time is: XX.XX", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                
                // TODO: so @fufu gets link from db and shows them with text
                // Soudout to a User / Twitch Channel
                new CommandAndResponse() {Command = "so", Response = "", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Undefined },
                
                // TODO: have an immage for showing all command and Platformn ID's for
                // For steam join the Group XXX | EA: XXX | Battlenet: XXX | XBOX for PC: XXX
                // If you are in Discord join the waiting room so we can pull you wen it is your turn
                new CommandAndResponse() {Command = "cday", Response = "if you want to join the Que use !joinq", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cinfo", Response = "if you want to join the Que use !joinq", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cjoin", Response = "Has have Joined the Queue on Possition", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cleve", Response = "Has have Left the Queue", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cwho", Response = "Current users are: ", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cnext", Response = "Next users are: ", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cremove", Response = "Users have been Removed from the Active Queue", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cqueue", Response = "You are in the Active Queue", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue},
                new CommandAndResponse() {Command = "clast", Response = "Has Been moved to last place in Queue", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cstart", Response = "Queue is now open to join", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue },
                new CommandAndResponse() {Command = "cend", Response = "Queue is closed and no one is able to join", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Queue },

                new CommandAndResponse() {Command = "rules", Response = "", Active = false, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "cc", Response = "Crowd Controll Links:", Active = false, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "vote", Response = "If you want to vote for me Here is the Link: ", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "modpack", Response = "There is no Modpack about this Category: ", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Game },
                new CommandAndResponse() {Command = "gameinfo", Response = "There is no Info about this Category: ", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Game },
                new CommandAndResponse() {Command = "gameprogress", Response = "There is no Progress about this Category: ", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Game },
                new CommandAndResponse() {Command = "song", Response = "There is no Info about this Song", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Song },

                // Adds timer for x time
                new CommandAndResponse() {Command = "timer", Response = "Timer has been set for X minutes", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },

                new CommandAndResponse() {Command = "sstart", Response = "Subathon timer has been started", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Subathon },
                new CommandAndResponse() {Command = "sstop", Response = "Subathon timer has been stoped", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Subathon },
                new CommandAndResponse() {Command = "sset", Response = "Subathon timer has been set to XXX", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Subathon },

                new CommandAndResponse() {Command = "streamstart", Response = "defines the start time of the stream in the db", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "streamstop", Response = "defines the end time of the stream in the db", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },

                // creates a twitch clip
                new CommandAndResponse() {Command = "clip", Response = "clips the last X Seconds", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },
                new CommandAndResponse() {Command = "clip2", Response = "Creates a time stamp of the current recording", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },

                //TODO: Fun chat things
                // flips chat message around
                new CommandAndResponse() {Command = "flip", Response = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun },
                new CommandAndResponse() {Command = "random", Response = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun },
                new CommandAndResponse() {Command = "rainbow", Response = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun },
                new CommandAndResponse() {Command = "revert", Response = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun },
                new CommandAndResponse() {Command = "bounce", Response = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun },
                new CommandAndResponse() {Command = "random", Response = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun },
                new CommandAndResponse() {Command = "translatehell", Response = "", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.fun },

                new CommandAndResponse() {Command = "clipper", Response = "Send some love to all the Clipper's ", Active = true, Auth = AuthEnum.undefined, Category = CategoryEnum.Undefined },

                new CommandAndResponse() {Command = "update", Response = "Chat Emotes have been updated", Active = true, Auth = AuthEnum.Mod, Category = CategoryEnum.Undefined },
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
    }
}
