using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.User;

namespace StreamingApp.Core.Queries.Logic.Interfaces;
public interface IMessageCheck
{
    Task<bool> Execute(MessageDto message, User user);
}