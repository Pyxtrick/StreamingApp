using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Chat;

public class StreamerChat
{
    ITwitchCallCache _twitchCallCache;

    public StreamerChat(ITwitchCallCache twitchCallCache)
    {
        _twitchCallCache = twitchCallCache;
    }

    // TODO: Verified / Streamer (everyone who raided the channel / Later how has X amount of Fallowers)
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
        var foundUser = new User();//_context.User.Include(TwitchDetail.firstOrDefault(t => t.UserName))).Include(Status);

        if (foundUser.Status.UserType == UserTypeEnum.Streamer || foundUser.Status.IsVerified)
        {
            return true;
        }

        return false;
    }
}
