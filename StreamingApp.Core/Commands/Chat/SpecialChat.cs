using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Chat;

public class SpecialChat
{
    ITwitchCallCache _twitchCallCache;

    public SpecialChat(ITwitchCallCache twitchCallCache)
    {
        _twitchCallCache = twitchCallCache;
    }

    // TODO: shows SubMessage, Highlighted and fist messages since stream start from each user
    public List<ChatDto> Execute(DateTime startTime, DateTime? endTime)
    {
        DateTime date = (DateTime)(endTime != null ? endTime : DateTime.Now);

        IReadOnlyList<ChatDto> allChat = (IReadOnlyList<ChatDto>)_twitchCallCache.GetAllMessagesFromTo(startTime, date, Domain.Enums.CallCacheEnum.CachedMessageData);

        List<ChatDto> FillterdAllChat = new List<ChatDto>();

        foreach (var chat in allChat)
        {
            if (chat.SpecialMessage.Contains(SpecialMessgeEnum.FirstMessage) ||
                chat.SpecialMessage.Contains(SpecialMessgeEnum.FirstStreamMessage) ||
                chat.SpecialMessage.Contains(SpecialMessgeEnum.SubMessage) ||
                chat.SpecialMessage.Contains(SpecialMessgeEnum.Highlighted))
            {
                FillterdAllChat.Add(chat);
            }
        }

        return FillterdAllChat;
    }
}
