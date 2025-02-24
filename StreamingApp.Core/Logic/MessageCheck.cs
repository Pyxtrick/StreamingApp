using StreamingApp.API.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;
using System.Text.RegularExpressions;

namespace StreamingApp.Core.Logic;

public class MessageCheck : IMessageCheck
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ISendRequest _twitchSendRequest;

    public MessageCheck(UnitOfWorkContext unitOfWork, ISendRequest twitchSendRequest)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = twitchSendRequest;
    }

    public bool Execute(MessageDto messageDto, User user)
    {
        var message = messageDto.Message;

        bool isFine = true;

        List<SpecialWords> foundSpecialWords = _unitOfWork.SpecialWords.Where(s => message.Contains(s.Name)).ToList();

        foreach (var t in foundSpecialWords.Where(f => f.Type == SpecialWordEnum.Count))
        {
            t.TimesUsed++;
        }

        foreach (var t in foundSpecialWords.Where(f => f.Type == SpecialWordEnum.Replace))
        {
            message.Replace(t.Name, "");
        }

        if (user.Status.UserType != UserTypeEnum.Bot
            && user.Status.UserType != UserTypeEnum.Mod
            && user.Status.UserType != UserTypeEnum.Streamer)
        {
            // For Dot Between two Texts (Text.Text)
            if (new Regex("([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?", RegexOptions.IgnoreCase).Match(message).Success)
            {
                isFine = false;
            }

            // For any staring < ending with >
            if(new Regex("(\\<).*([\\w-]).*(\\>)", RegexOptions.IgnoreCase).Match(message).Success)
            {
                isFine = false;
            }

            if (foundSpecialWords.Any(f => f.Type == SpecialWordEnum.Delete))
            {
                // TODO: allow User for x Seconds to use
                // TODO: Delete Messate

                //_twitchSendRequest.DeleteMessage(messageDto.MessageId)

                user.Ban.MessagesDeletedAmount++;

                isFine = false;
            }

            if (foundSpecialWords.Any(f => f.Type == SpecialWordEnum.Timeout))
            {
                // TODO: Time Out User

                var specialWords = foundSpecialWords.First(f => f.Type == SpecialWordEnum.Timeout);
                
                // TODO Use specialWords.Comment to determin timeout time

                //_twitchSendRequest.TimeoutUser(messageDto.UserId, $"Used prohibited word {specialWords.Name}", )

                user.Ban.TimeOutAmount++;

                isFine = false;
            }

            if (foundSpecialWords.Any(f => f.Type == SpecialWordEnum.Banned))
            {
                // TODO: Ban User

                //_twitchSendRequest.BanUser(messageDto.UserId, $"Used prohibited word {foundSpecialWords.First(f => f.Type == SpecialWordEnum.Banned).Name})

                user.Ban.IsBaned = true;
                user.Ban.BanedDate = DateTime.Now;
                user.Ban.LastMessage = message;
                user.Ban.BanedAmount++;

                isFine = false;
            }
        }

        _unitOfWork.SaveChanges();

        return isFine;
    }
}
