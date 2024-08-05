using StreamingApp.Domain.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.Core.Commands.Chat;
public class BotChat
{
    // only show users with the bot Tag
    // for checking for any problems or miss use
    public List<ChatDto> Execute()
    {
        // TODO: get from some where
        // List of users that are not to bee shown in the main Chat like bots
        List<string> userNameList = new List<string>
        {
            "PyxtrickBot",
            "Nightbot", // Can also work with youtube
            "Fossabot",
            "Moobot",
            "StreamElements",
            "SoundAlerts",
            "Sery_bot",
            "Streamlabs",
            "TangiaBot",
        };

        List<ChatDto> FillterdAllChat = new List<ChatDto>();

        return FillterdAllChat;
    }
}
