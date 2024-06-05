using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Chat;

public class ModChat
{
    ITwitchCallCache _twitchCallCache;

    public ModChat(ITwitchCallCache twitchCallCache)
    {
        _twitchCallCache = twitchCallCache;
    }

    // TODO: Streamer / Mod Chat / TwitchStaff (only able to see messages from specific people)
    public List<ChatDto> Execute(DateTime startTime, DateTime? endTime)
    {
        DateTime date = (DateTime)(endTime != null ? endTime : DateTime.Now);

        IReadOnlyList<ChatDto> allChat = (IReadOnlyList<ChatDto>)_twitchCallCache.GetAllMessagesFromTo(startTime, date, Domain.Enums.CallCacheEnum.CachedMessageData);

        List<ChatDto> FillterdAllChat = new List<ChatDto>();

        foreach (var chat in allChat)
        {
            if (chat.Auth.Contains(AuthEnum.Streamer) || chat.Auth.Contains(AuthEnum.Mod) || chat.Auth.Contains(AuthEnum.Staff))
            {
                FillterdAllChat.Add(chat);
            }
        }

        return FillterdAllChat;
    }
}
