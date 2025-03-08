using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.DB.Interfaces;
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

    private readonly IUpdateUserOnDB _updateUserOnDB;

    public ManageDeleted(ISendSignalRMessage sendSignalRMessage, ITwitchCallCache twitchCallCache, IUpdateUserOnDB updateUserOnDB)
    {
        _sendSignalRMessage = sendSignalRMessage;
        _twitchCallCache = twitchCallCache;
        _updateUserOnDB = updateUserOnDB;
    }

    public async Task Execute(BannedUserDto bannedUserDto)
    {
        var messages = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedMessageData).ConvertAll(s => (MessageDto)s);

        if (bannedUserDto.TargetEnum == BannedTargetEnum.Message)
        {
            bannedUserDto.UserId = messages.FirstOrDefault(m => m.MessageId.Equals(bannedUserDto.MessageId)).UserId;

            await _updateUserOnDB.UpdateBan(bannedUserDto.UserId, bannedUserDto);

            await _sendSignalRMessage.SendBannedEventMessage(bannedUserDto);
            return;
        }
        else if (bannedUserDto.TargetEnum == BannedTargetEnum.Message || bannedUserDto.TargetEnum == BannedTargetEnum.Message)
        {
            messages = messages.Where(m => m.UserId.Equals(bannedUserDto.UserId)).ToList();

            bannedUserDto.LastMessage = messages.Last().Message;

            await _updateUserOnDB.UpdateBan(bannedUserDto.UserId, bannedUserDto);
        }

        foreach (var message in messages)
        {
            BannedUserDto bannedMessage = new(message.UserId, message.MessageId, message.UserName, message.Message, bannedUserDto.Reson, bannedUserDto.TargetEnum, message.Date);

            await _sendSignalRMessage.SendBannedEventMessage(bannedMessage);
        }
    }
}
