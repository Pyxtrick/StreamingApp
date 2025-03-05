using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.Interfaces;

public interface IUpdateUserOnDB
{
    Task UpdateAchievements(int userId);

    Task UpdateAuth(int userId, List<AuthEnum> auths);

    Task UpdateSub(int userId, bool isSub, TierEnum tier, int subTime);
}