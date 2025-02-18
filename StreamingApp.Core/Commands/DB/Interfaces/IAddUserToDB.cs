using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.DB.Interfaces;

public interface IAddUserToDB
{
    Task<int> AddUser(string twitchUserId, string userName, bool isSub, int subTime, List<AuthEnum> auth);
}