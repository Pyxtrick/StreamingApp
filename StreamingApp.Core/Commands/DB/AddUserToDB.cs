using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.Interfaces;
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
        User user = _unitOfWork.User.Include("TwitchDetail").Where(t => t.TwitchDetail.UserId == twitchUserId).ToList().FirstOrDefault();

        if (user == null)
        {
            // TODO: make a Class for this in API.Twitch
            //GetHypeTrainResponse hypeTrain = await _twitchCache.GetTheTwitchAPI().Helix.HypeTrain.GetHypeTrainEventsAsync(_configuration["Twitch:ClientId"], 1, null);

            User newUser = new User()
            {
                TwitchDetail = new()
                {
                    UserId = twitchUserId,
                    UserName = userName,
                    AppAuthEnum = AppAuthEnum.user
                },
                TwitchAchievements = new()
                {
                    FirstStreamSeen = DateTime.Now,
                    LastStreamSeen = DateTime.Now,
                    GiftedSubsCount = 0,
                    GiftedBitsCount = 0,
                    GiftedDonationCount = 0,
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
                    IsVerified = auth.FirstOrDefault(e => e == AuthEnum.Partner) == AuthEnum.Partner,
                    TimeZone = "waiting"
                },
                Ban = new()
                {
                    IsBaned = false,
                    IsExcludeQueue = false,
                    ExcludePole = false,
                    IsExcludeChat = false,
                    ExcludeReason = "",
                    TimeOutAmount = 0,
                    MessagesDeletedAmount = 0,
                    BanedAmount = 0,
                    BanedDate = null,
                    LastMessage = ""
                }
            };

            _unitOfWork.User.Add(newUser);
            await _unitOfWork.SaveChangesAsync();

            return newUser.Id;
        }

        return user.Id;
    }
}
