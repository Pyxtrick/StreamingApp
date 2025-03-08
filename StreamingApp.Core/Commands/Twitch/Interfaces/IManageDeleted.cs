using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Commands.Twitch.Interfaces;
public interface IManageDeleted
{
    Task Execute(BannedUserDto bannedUserDto);
}