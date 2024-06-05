using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public interface ICheck
{
    bool CheckAuth(AuthEnum messageAuth, List<AuthEnum> userAuth);

    bool CheckIfCommandAvalibleToUse(string message, List<AuthEnum> auth);
}