using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDUsers
{
    Task<List<UserDto>> GetAll();

    Task<User> CreateOne(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth, ChatOriginEnum chatOrigin);

    Task CombineUser(string twitchUserId, string youtubeUserId);

    Task<bool> UpdateAchievements(string userId, ChatOriginEnum chatOrigin);

    Task<bool> UpdateAuth(string userId, List<AuthEnum> auths, ChatOriginEnum chatOrigin);

    Task<bool> UpdateSub(string userId, bool isSub, TierEnum tier, int subTime, ChatOriginEnum chatOrigin);

    Task<bool> UpdateBan(string userId, BannedUserDto bannedUserDto, ChatOriginEnum chatOrigin);

    Task<bool> Delete(List<UserDto> commands, ChatOriginEnum chatOrigin);
}