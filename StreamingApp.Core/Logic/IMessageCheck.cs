using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.User;

namespace StreamingApp.Core.Logic;
public interface IMessageCheck
{
    bool Execute(MessageDto message, User user);
}