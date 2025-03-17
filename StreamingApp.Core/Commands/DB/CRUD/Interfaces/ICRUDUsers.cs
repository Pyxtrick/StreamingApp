using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDUsers
{
    List<UserDto> GetAll();

    Task<User> CreateOne(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth);
    
    Task UpdateAchievements(string userId);

    Task UpdateAuth(string userId, List<AuthEnum> auths);

    Task UpdateSub(string userId, bool isSub, TierEnum tier, int subTime);

    Task UpdateBan(string userId, BannedUserDto bannedUserDto);

    bool Delete(List<UserDto> commands);
}