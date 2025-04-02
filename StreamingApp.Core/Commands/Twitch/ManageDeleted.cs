using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Core.Commands.Hub;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class ManageDeleted : IManageDeleted
{
    private readonly ISendSignalRMessage _sendSignalRMessage;

    private readonly ITwitchCallCache _twitchCallCache;

    private readonly ICRUDUsers _crudUsers;

    public ManageDeleted(ISendSignalRMessage sendSignalRMessage, ITwitchCallCache twitchCallCache, ICRUDUsers crudUsers)
    {
        _sendSignalRMessage = sendSignalRMessage;
        _twitchCallCache = twitchCallCache;
        _crudUsers = crudUsers;
    }

    public async Task Execute(BannedUserDto bannedUserDto)
    {
        var messages = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedMessageData, true).ConvertAll(s => (MessageDto)s);

        if (bannedUserDto.TargetEnum == BannedTargetEnum.Message)
        {
            bannedUserDto.UserId = messages.FirstOrDefault(m => m.MessageId.Equals(bannedUserDto.MessageId)).UserId;

            await _crudUsers.UpdateBan(bannedUserDto.UserId, bannedUserDto);

            await _sendSignalRMessage.SendBannedEventMessage(bannedUserDto);
            return;
        }
        else if (bannedUserDto.TargetEnum == BannedTargetEnum.Message || bannedUserDto.TargetEnum == BannedTargetEnum.Message)
        {
            messages = messages.Where(m => m.UserId.Equals(bannedUserDto.UserId)).ToList();

            bannedUserDto.LastMessage = messages.Last().Message;

            await _crudUsers.UpdateBan(bannedUserDto.UserId, bannedUserDto);
        }

        foreach (var message in messages)
        {
            BannedUserDto bannedMessage = new(message.UserId, message.MessageId, message.UserName, message.Message, bannedUserDto.Reson, bannedUserDto.TargetEnum, false, message.Date);

            await _sendSignalRMessage.SendBannedEventMessage(bannedMessage);
        }
    }
}
