using Microsoft.EntityFrameworkCore;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;
using TwitchLib.Client.Models;

namespace StreamingApp.Core.Commands.DB;

public class UpdateSub
{
    private readonly UnitOfWorkContext _unitOfWork;

    public UpdateSub(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task Execute(Subscriber? subscriber, PrimePaidSubscriber? primePaidSubscriber)
    {
        var userId = subscriber != null ? subscriber.UserId : primePaidSubscriber.UserId;

        User data = _unitOfWork.User.Include("TwitchDetail").Where(t => t.TwitchDetail.UserId == userId).Include("Status.TwitchSub").ToList().First();

        Sub sub = data.Status.TwitchSub;

        var t = subscriber != null ? int.Parse(subscriber.MsgParamCumulativeMonths) : int.Parse(primePaidSubscriber.MsgParamCumulativeMonths);
        var tier = subscriber != null ? subscriber.SubscriptionPlan.ToString() : primePaidSubscriber.SubscriptionPlan.ToString();

        sub.CurrentySubscribed = subscriber.IsSubscriber;
        sub.SubscribedTime = t;
        sub.CurrentTier = (TierEnum)Enum.Parse(typeof(TierEnum), tier);

        _unitOfWork.Sub.Add(sub);
        await _unitOfWork.SaveChangesAsync();
    }
}
