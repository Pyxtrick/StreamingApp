using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Commands.Chat;

public class AllertsChat
{
    ITwitchCallCache _twitchCallCache;

    public AllertsChat(ITwitchCallCache twitchCallCache)
    {
        _twitchCallCache = twitchCallCache;
    }

    // TODO: shows Allerts like sub, giffted sub, resub, prime, bits, raid, redeem
    // wo has done what (X has giffted X subs)
    public List<ChatDto> Execute(DateTime startTime, DateTime? endTime)
    {
        DateTime date = (DateTime)(endTime != null ? endTime : DateTime.Now);

        //IReadOnlyList<ChatDto> allChat = _chatCache.GetChatData(startTime, date);

        List<ChatDto> FillterdAllChat = new List<ChatDto>();

        /**foreach (var chat in allChat)
        {

        }**/

        return FillterdAllChat;
    }
}
