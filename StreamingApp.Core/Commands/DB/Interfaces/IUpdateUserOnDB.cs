using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.Interfaces;

public interface IUpdateUserOnDB
{
    Task UpdateAchievements(string userId);

    Task UpdateAuth(string userId, List<AuthEnum> auths);

    Task UpdateSub(string userId, bool isSub, TierEnum tier, int subTime);

    Task UpdateBan(string userId, BannedUserDto bannedUserDto);
}