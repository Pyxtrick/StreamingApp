using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class ManageAchievements : IManageAchievements
{
    private readonly IManageFile _manageFile;

    private readonly UnitOfWorkContext _unitOfWork;

    public ManageAchievements(IManageFile manageFile, UnitOfWorkContext unitOfWork)
    {
        _manageFile = manageFile;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteSub(SubDto sub)
    {
        var user = _unitOfWork.User.Include("Achievements").Include("Details").Include("Status").Include("Status.Subs").FirstOrDefault(u => u.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).ExternalUserId == sub.UserId);

        if (sub.IsGifftedSub)
        {
            var twichAchievements = user.Achievements.FirstOrDefault(t => t.Origin == sub.Origin);

            twichAchievements.GiftedSubsCount += sub.GifftedSubCount;

            //_unitOfWork.Achievements.Update(twichAchievements);
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            var twitchSub = user.Status.Subs.FirstOrDefault(t => t.Origin != sub.Origin);

            twitchSub.CurrentySubscribed = true;
            twitchSub.CurrentTier = sub.CurrentTier;

            //_unitOfWork.Sub.Update(twitchSub);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task ExecuteBit(MessageAlertDto alertDto)
    {
        var user = _unitOfWork.User.Include("Achievements").FirstOrDefault(u => u.Details.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).ExternalUserId == alertDto.UserId);

        var twichAchievements = user.Achievements.FirstOrDefault(t => t.Origin == alertDto.origin);

        twichAchievements.GiftedBitsCount += alertDto.Bits;

        //_unitOfWork.Achievements.Update(twichAchievements);
        await _unitOfWork.SaveChangesAsync();
    }
}
