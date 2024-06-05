using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Commands.Chat;

public class MensionChat
{
    ITwitchCallCache _twitchCallCache;

    public MensionChat(ITwitchCallCache twitchCallCache)
    {
        _twitchCallCache = twitchCallCache;
    }

    // TODO: Mensions of Streamer or Mods (only able to see messages from specific people)
    public List<ChatDto> Execute(DateTime startTime, DateTime? endTime)
    {
        DateTime date = (DateTime)(endTime != null ? endTime : DateTime.Now);

        IReadOnlyList<ChatDto> allChat = (IReadOnlyList<ChatDto>)_twitchCallCache.GetAllMessagesFromTo(startTime, date, Domain.Enums.CallCacheEnum.CachedMessageData);

        List<ChatDto> FillterdAllChat = new List<ChatDto>();

        foreach (var chat in allChat)
        {
            if (CheckIfValid(chat))
            {
                FillterdAllChat.Add(chat);
            }
        }

        return FillterdAllChat;
    }

    private bool CheckIfValid(ChatDto chat)
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
        };

        if (userNameList.FirstOrDefault(t => t.Contains(chat.UserName)) == null)
        {
            return true;
        }

        return false;
    }
}
