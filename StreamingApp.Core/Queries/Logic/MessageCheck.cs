using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StreamingApp.API.Interfaces;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Queries.Logic.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;
using System.Text.RegularExpressions;

namespace StreamingApp.Core.Queries.Logic;

public class MessageCheck : IMessageCheck
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ITwitchSendRequest _twitchSendRequest;

    private readonly IHubContext<ChatHub> _hubContext;

    public MessageCheck(UnitOfWorkContext unitOfWork, ITwitchSendRequest twitchSendRequest, IHubContext<ChatHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = twitchSendRequest;
        _hubContext = hubContext;
    }

    /// <summary>
    /// Checks Message if there is any thing that is not to be allowed to show in the Frotnend
    /// </summary>
    /// <param name="messageDto"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<bool> Execute(MessageDto messageDto, User user)
    {
        bool isFine = true;

        List<SpecialWords> foundSpecialWords = _unitOfWork.SpecialWords.Where(s => messageDto.Message.Contains(s.Name)).ToList();

        bool isAllowed = foundSpecialWords.Where(s => messageDto.Message.StartsWith(s.Name)).Any(fsw => fsw.Type == SpecialWordEnum.AllowedUrl);

        foreach (var t in foundSpecialWords.Where(f => f.Type == SpecialWordEnum.Count))
        {
            t.TimesUsed++;
        }

        foreach (var t in foundSpecialWords.Where(f => f.Type == SpecialWordEnum.Replace))
        {
            messageDto.Message = messageDto.Message.Replace(t.Name, "");
        }

        if (user.Status.UserType != UserTypeEnum.Bot
            && user.Status.UserType != UserTypeEnum.Mod
            && user.Status.UserType != UserTypeEnum.Streamer)
        {
            // For Dot Between two Texts (Text.Text)
            if (new Regex("([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?", RegexOptions.IgnoreCase).Match(messageDto.Message).Success)
            {
                isFine = false;
            }

            // For any staring < ending with >
            if (new Regex("(\\<).*([\\w-]).*(\\>)", RegexOptions.IgnoreCase).Match(messageDto.Message).Success)
            {
                isFine = false;
            }

            if (foundSpecialWords.Any(f => f.Type == SpecialWordEnum.Delete))
            {
                if (foundSpecialWords.Any(f => f.Name.Contains("http")))
                {
                    var foundHttp = messageDto.Message.Split(" ").Where(s => s.Contains("http"));

                    if (foundHttp.Count() == 1)
                    {
                        if (foundSpecialWords.Any(f => f.Type == SpecialWordEnum.AllowedUrl) == false)
                        {
                            await _twitchSendRequest.DeleteMessage(messageDto.MessageId);
                        }
                    }
                    else
                    {
                        foreach (var fh in foundHttp)
                        {
                            var t = foundSpecialWords.Where(f => fh.Contains(f.Name));

                            if (t.Count() <= 2)
                            {
                                await _twitchSendRequest.DeleteMessage(messageDto.MessageId);
                            }
                        }
                    }
                }
                else if (!isAllowed)
                {
                    // TODO: allow User for x Seconds to use
                    // TODO: Delete Messate
                    await _twitchSendRequest.DeleteMessage(messageDto.MessageId);

                    user.Ban.MessagesDeletedAmount++;
                }

                isFine = false;
            }

            if (foundSpecialWords.Any(f => f.Type == SpecialWordEnum.Timeout))
            {
                if (!isAllowed)
                {
                    List<string> result = (from word in foundSpecialWords.Where(f => f.Type == SpecialWordEnum.Timeout) select word.Name.ToString()).ToList();

                    int timeOutSeconds = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == messageDto.Origin)!.TimeOutSeconds;

                    // TODO Use specialWords.Comment to determin timeout time
                    await _twitchSendRequest.TimeoutUser(messageDto.UserId, $"Used prohibited word {JsonConvert.SerializeObject(result)}", timeOutSeconds);

                    user.Ban.TimeOutAmount++;
                }

                isFine = false;
            }

            if (foundSpecialWords.Any(f => f.Type == SpecialWordEnum.Banned))
            {
                if (!isAllowed)
                {
                    // TODO: Ban User

                    //_twitchSendRequest.BanUser(messageDto.UserId, $"Used prohibited word {foundSpecialWords.First(f => f.Type == SpecialWordEnum.Banned).Name})

                    user.Ban.IsBaned = true;
                    user.Ban.BanedDate = DateTime.Now;
                    user.Ban.LastMessage = messageDto.Message;
                    user.Ban.BanedAmount++;
                }

                isFine = false;
            }
        }

        if (isFine == false && isAllowed)
        {
            messageDto.Message += "";

            await _hubContext.Clients.All.SendAsync("ReceiveWatchUserMessage", messageDto);
        }

        _unitOfWork.SaveChanges();

        return isFine;
    }
}
