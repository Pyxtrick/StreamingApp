using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDUsers
{
    Task<List<UserDto>> GetAll();

    Task<User> CreateOne(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth, OriginEnum Origin);

    Task CombineUser(string twitchUserId, string youtubeUserId);

    Task<bool> UpdateAchievements(string userId, OriginEnum Origin);

    Task<bool> UpdateAuth(string userId, List<AuthEnum> auths, OriginEnum Origin);

    Task<bool> UpdateSub(string userId, bool isSub, TierEnum tier, int subTime, OriginEnum Origin);

    Task<bool> UpdateBan(string userId, BannedUserDto bannedUserDto, OriginEnum Origin);

    Task<bool> Delete(List<UserDto> commands, OriginEnum Origin);
}