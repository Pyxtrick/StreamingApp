using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class PointRedeam : IPointRedeam
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IHubContext<ChatHub> _hubContext;

    private readonly ICRUDUsers _crudUsers;

    public PointRedeam(UnitOfWorkContext unitOfWork, IHubContext<ChatHub> hubContext, ICRUDUsers crudUsers)
    {
        _unitOfWork = unitOfWork;
        _hubContext = hubContext;
        _crudUsers = crudUsers;
    }

    public async Task Execute(string userName, string userId, string rewardid, string rewardName, string rewardPrompt)
    {
        await _crudUsers.UpdateAchievements(userId, OriginEnum.Twitch);

        var trigger = _unitOfWork.Trigger.Include("Targets").FirstOrDefault(t => t.TriggerCondition == Domain.Enums.Trigger.TriggerCondition.Redeam && t.Description == rewardName);
        
        if(trigger != null)
        {
        
        }

        string message = $"{userName} Redeamed {rewardName}";

        MessageDto chatMessage = new(false, "channel", "#ff6b6b", null, message, message, new(), new(), OriginEnum.Twitch, new() { AuthEnum.Undefined }, new(), EffectEnum.none, false, 0, false, "messageId", userId, userName, userName, DateTime.Now);
        await _hubContext.Clients.All.SendAsync("ReceiveEventMessage", chatMessage);
    }
}
