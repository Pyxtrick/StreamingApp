using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB;
using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDUsers
{
    Task<List<UserDto>> GetAll();

    Task<User> CreateOne(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth);
    
    Task<bool> UpdateAchievements(string userId);

    Task<bool> UpdateAuth(string userId, List<AuthEnum> auths);

    Task<bool> UpdateSub(string userId, bool isSub, TierEnum tier, int subTime);

    Task<bool> UpdateBan(string userId, BannedUserDto bannedUserDto);

    Task<bool> Delete(List<UserDto> commands);
}