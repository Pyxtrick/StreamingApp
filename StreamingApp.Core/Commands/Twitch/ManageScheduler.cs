using StreamingApp.API.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class ManageScheduler : IManageScheduler
{
    private readonly ISendRequest _twitchSendRequest;

    private readonly ITwitchCallCache _twitchCallCache;

    public ManageScheduler(ISendRequest twitchSendRequest, ITwitchCallCache twitchCallCache)
    {
        _twitchSendRequest = twitchSendRequest;
        _twitchCallCache = twitchCallCache;
    }

    public async Task Execute(Trigger trigger)
    {
        Console.WriteLine("test");
        // Prevent Chat spaming by the Bot
        if(trigger.ScheduleTime < 30)
        {
            var messageCount = 0;
            List<object> value = _twitchCallCache.GetAllUnusedMessages(CallCacheEnum.CachedMessageData);

            if (value.Count != 0)
            {
                List<MessageDto> messages = value.ConvertAll(s => (MessageDto)s);

                messageCount = messages.Count;
            }

            if (messageCount < 20)
            {
                return;
            }
        }

        foreach (var target in trigger.Targets)
        {
            _twitchSendRequest.SendChatMessage(target.CommandAndResponse.Response);
        }
    }
}
