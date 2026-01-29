using StreamingApp.API.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class ManageScheduler : IManageScheduler
{
    private readonly ITwitchSendRequest _twitchSendRequest;

    private readonly IYouTubeSendRequest _youTubeSendRequest;

    private readonly ITwitchCallCache _twitchCallCache;

    private readonly UnitOfWorkContext _unitOfWork;

    public ManageScheduler(ITwitchSendRequest twitchSendRequest, ITwitchCallCache twitchCallCache, IYouTubeSendRequest youTubeSendRequest, UnitOfWorkContext unitOfWorkContext)
    {
        _twitchSendRequest = twitchSendRequest;
        _twitchCallCache = twitchCallCache;
        _youTubeSendRequest = youTubeSendRequest;
        _unitOfWork = unitOfWorkContext;
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
            var stream = _unitOfWork.StreamHistory.OrderBy(s => s.StreamStart).Last();

            var message = target.CommandAndResponse.Response;

            await _twitchSendRequest.SendChatMessage(message);
            await _youTubeSendRequest.SendChatMessage(message);
        }
    }
}
