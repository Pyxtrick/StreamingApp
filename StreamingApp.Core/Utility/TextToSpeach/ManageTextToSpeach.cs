using StreamingApp.API.TTS;
using StreamingApp.Core.Utility.TextToSpeach.Cache;
using StreamingApp.DB;
using StreamingApp.Domain.Entities;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Utility.TextToSpeach;

public class ManageTextToSpeach : IManageTextToSpeach
{
    private readonly ITTSApiRequest _TTSApiRequest;

    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ITTSCache _ttsCache;

    public ManageTextToSpeach(ITTSApiRequest tTSApiRequest, UnitOfWorkContext unitOfWork, ITTSCache ttsCache)
    {
        _TTSApiRequest = tTSApiRequest;
        _unitOfWork = unitOfWork;
        _ttsCache = ttsCache;
    }

    public void Execute(string message, ChatOriginEnum chatOrigin)
    {
        string convertedMessage = "";

        convertedMessage = ShortenMessage(message);

        convertedMessage = RemoveDuplicateText(convertedMessage);

        convertedMessage = CencorTextInMessage(convertedMessage);

        var count = convertedMessage.Split(' ').Count();

        TTSData data = new TTSData()
        {
            Message = convertedMessage,
            OriginalMessage = message,
            MessageLengthSeconds = count,
            IsActive = true,
            Posted = DateTime.UtcNow,
        };

        _ttsCache.AddTTSData(data);
    }

    public async Task PlayTTSMessage()
    {
        var data = _ttsCache.GetLatestTTSData();

        await _TTSApiRequest.SendMessage(data);
    }

    private string ShortenMessage(string message)
    {
        var ttsLenghtAmmount = _unitOfWork.Settings.Where(s => s.Origin == ChatOriginEnum.Twitch).FirstOrDefault().TTSLenghtAmmount;

        string newMessage = "";
        var split = message.Split(' ');

        for (int i = 0; i < ttsLenghtAmmount; i++)
        {
            newMessage += split[i] + " ";
        }

        return newMessage;
    }

    private string RemoveDuplicateText(string message)
    {
        string newMessage = "";
        var ttsSpamAmmount = _unitOfWork.Settings.Where(s => s.Origin == ChatOriginEnum.Twitch).FirstOrDefault().TTSSpamAmmount;

        List<KeyValuePair<string, int>> wordCount = new();

        foreach (var word in message.Split(' '))
        {
            KeyValuePair<string, int> found = wordCount.FirstOrDefault(w => w.Key.Contains(word));

            if (found.Value > ttsSpamAmmount)
            {
                return newMessage;
            }

            if (found.Key != null)
            {
                found = new(found.Key, found.Value + 1);
                wordCount.Remove(found);
            }
            else
            {
                found = new(word, 1);
            }

            newMessage += word + " ";
            wordCount.Add(found);
        }

        return message;
    }

    private string CencorTextInMessage(string message)
    {
        var specialWords = _unitOfWork.SpecialWords.Where(s => s.Type <= SpecialWordEnum.Replace).ToList();

        foreach (var specialWord in specialWords)
        {
            message = message.Replace(specialWord.Name, "...");
        }

        return message;
    }
}
