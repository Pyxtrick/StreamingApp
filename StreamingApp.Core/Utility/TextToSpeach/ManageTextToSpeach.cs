using StreamingApp.API.TTS;
using StreamingApp.Core.Utility.TextToSpeach.Cache;
using StreamingApp.DB;
using StreamingApp.Domain.Entities;
using StreamingApp.Domain.Entities.Dtos.Twitch;
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

    public void Execute(MessageDto messageDto)
    {
        string convertedMessage = "";

        convertedMessage = RemoveDuplicateText(messageDto.Message);

        convertedMessage = ShortenMessage(convertedMessage);

        convertedMessage = CencorTextInMessage(convertedMessage);

        var count = convertedMessage.Split(' ').Count();

        TTSData data = new TTSData()
        {
            Message = convertedMessage,
            OriginalMessage = messageDto.Message,
            MessageLengthSeconds = count,
            IsActive = true,
            Posted = DateTime.UtcNow,
        };

        _ttsCache.AddTTSData(data);
    }

    public async Task PlaySpecificTTSMessage(int id)
    {
        var data = _ttsCache.GetSpecificTTSData(id);

        await _TTSApiRequest.SendMessage(data);
    }

    public async Task PlayLatestTTSMessage()
    {
        var data = _ttsCache.GetLatestTTSData();

        await _TTSApiRequest.SendMessage(data);
    }

    private string ShortenMessage(string message)
    {
        var ttsLenghtAmmount = _unitOfWork.Settings.Where(s => s.Origin == ChatOriginEnum.Twitch).FirstOrDefault().TTSLenghtAmmount;

        string newMessage = "";
        var split = message.Split(' ');

        // Start at 1 to remvoe !say
        int startIndex = split[0].Contains("!") ? 1 : 0;

        for (int i = startIndex; i < ttsLenghtAmmount; i++)
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

            if (found.Key != null)
            {
                found = new(found.Key, found.Value + 1);
                wordCount.Remove(found);
            }
            else
            {
                found = new(word, 1);
            }

            // Do not Add Words That are used in a "Spam" way
            if (found.Value <= ttsSpamAmmount)
            {
                newMessage += word + " ";
            }

            wordCount.Add(found);
        }

        return newMessage;
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
