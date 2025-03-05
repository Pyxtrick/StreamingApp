using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.Interfaces;

public interface IAddUserToDB
{
    Task<User> AddUser(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth);
}