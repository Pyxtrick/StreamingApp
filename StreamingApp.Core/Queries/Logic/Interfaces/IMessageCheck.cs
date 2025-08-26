using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.User;

namespace StreamingApp.Core.Queries.Logic.Interfaces;
public interface IMessageCheck
{
    Task<bool> ExecuteMessageOnly(string message);

    Task<bool> Execute(MessageDto message, User user);
}