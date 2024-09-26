using Microsoft.EntityFrameworkCore;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB;

public class AddUserToDB : IAddUserToDB
{
    private readonly UnitOfWorkContext _unitOfWork;

    public AddUserToDB(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> AddUser(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth)
    {
        User user = _unitOfWork.User.Include("TwitchDetail").Where(t => t.TwitchDetail.UserId == twitchUserId).ToList().First();

        if (user == null)
        {
            // TODO: make a Class for this in API.Twitch
            //GetHypeTrainResponse hypeTrain = await _twitchCache.GetTheTwitchAPI().Helix.HypeTrain.GetHypeTrainEventsAsync(_configuration["Twitch:ClientId"], 1, null);

            User newUser = new User()
            {
                TwitchDetail = new()
                {
                    UserId = twitchUserId,
                    UserName = userName
                },
                TwitchAchievements = new()
                {
                    LastStreamSeen = DateTime.Now,
                    WachedStreams = 1
                },
                Status = new()
                {
                    TwitchSub = new()
                    {
                        CurrentySubscribed = isSub,
                        SubscribedTime = subTime,
                        CurrentTier = isSub ? TierEnum.Tier1 : TierEnum.None, // cannot get data
                    },
                    FirstChatDate = DateTime.Now,
                    FallowDate = DateTime.Now, // cannot get data
                    IsVIP = auth.FirstOrDefault(e => e == AuthEnum.Vip) == AuthEnum.Vip,
                    IsVerified = auth.FirstOrDefault(e => e == AuthEnum.Partner) == AuthEnum.Partner
                },
            };

            _unitOfWork.User.Add(newUser);
            await _unitOfWork.SaveChangesAsync();

            return newUser.Id;
        }

        return user.Id;
    }
}
