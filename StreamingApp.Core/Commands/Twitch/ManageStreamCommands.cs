using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Utility;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class ManageStreamCommands : IManageStreamCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ITwitchSendRequest _twitchSendRequest;

    private readonly IStreamHandler _streamStreamHandler;

    private readonly IManageStream _manageStream;

    public ManageStreamCommands(UnitOfWorkContext unitOfWork, ITwitchSendRequest twitchSendRequest, IStreamHandler streamHandler, IManageStream manageStream)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = twitchSendRequest;
        _streamStreamHandler = streamHandler;
        _manageStream = manageStream;
    }

    public async Task Execute(MessageDto messageDto)
    {
        // removed first word of string
        var noCommandTextMessage = messageDto.Message.Split().Length > 1 ? messageDto.Message[(messageDto.Message.Split()[0].Length + 1)..] : "";

        var splitMessage = messageDto.Message.Split(' ');

        string commandText = messageDto.Message.Split(' ').ToList()[0].Trim('!');

        var splitNoCommandTextMessage = noCommandTextMessage.Split(',');

        // TODO: Add Auth Check to the other Get CommandAndResponse to
        CommandAndResponse? commandAndResponse = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Command.Contains(commandText) && t.Active);

        if (splitMessage[0].Equals("!clip"))
        {
            var stream = _unitOfWork.StreamHistory.OrderBy(sh => sh.Id).ToList().Last();

            // TODO: Twitch clip Stream for the last x seconds
            var sh = await _twitchSendRequest.CreateClip(stream.StreamTitle);

            sh.StreamId = stream.Id;
            sh.Stream = stream;

            await _unitOfWork.StreamHighlights.AddAsync(sh);
            await _unitOfWork.SaveChangesAsync();

        }
        // Limit Commands to Mods and Streamer
        else if (messageDto.Auth.Min() <= AuthEnum.Mod)
        {
            if (splitMessage[0].Equals("!startStream"))
            {
                await _streamStreamHandler.Start();
            }
            else if (splitMessage[0].Equals("!stopStream"))
            {
                await _streamStreamHandler.Stop();
            }
            else if (splitMessage[0].Equals("!updateCategory"))
            {
                await _manageStream.ChangeCategory();
            }
            else if (splitMessage[0].Equals("!updatetitle"))
            {
                // TODO: Update Twitch Stream Title
                //_twitchSendRequest.UpdateStreamTitle(splitNoCommandTextMessage);
            }
            else if (splitMessage[0].Equals("!so"))
            {
                // TODO: Twitch Shouout user
                if (splitMessage[1] != null)
                {
                    if (messageDto.Origin == OriginEnum.Youtube)
                    {
                        //_youtubeSendRequest.SendChatMessage($"");
                        return;
                    }
                    var userDetail = _unitOfWork.UserDetail.FirstOrDefault(u => u.UserName == splitMessage[1] && u.Origin == OriginEnum.Twitch);

                    ChannelInfo? channelInfo = await _twitchSendRequest.GetChannelInfo(userDetail.ExternalUserId, true);

                    if (channelInfo != null)
                    {
                        string message = commandAndResponse.Response;

                        message = message.Replace("[User]", splitMessage[1]);
                        message = message.Replace("[GameName]", channelInfo.GameName);
                        message = message.Replace("[Url]", $"https://twitch.tv/{splitMessage[1]}");

                        _twitchSendRequest.SendAnnouncement(message);
                    }
                }
            }
            else if (splitMessage[0].Equals("!pole"))
            {
                if (splitNoCommandTextMessage.Length >= 3)
                {
                    await _manageStream.CreatePole(splitNoCommandTextMessage);
                }
                else
                {
                    await _twitchSendRequest.SendChatMessage($"pole needs more information (title, option1, option2)");
                }
            }
            else if (splitMessage[0].Equals("!prediction"))
            {
                if (splitNoCommandTextMessage.Length >= 3)
                {
                    await _manageStream.CreatePrediction(splitNoCommandTextMessage);
                }
                else
                {
                    await _twitchSendRequest.SendChatMessage($"prediction needs more information (title, option1, option2)");
                }
            }
        }
    }
}
