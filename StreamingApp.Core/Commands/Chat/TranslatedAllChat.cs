using LanguageDetection;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Commands.Chat;

public class TranslatedAllChat
{
    ITwitchCallCache _twitchCallCache;

    public TranslatedAllChat(ITwitchCallCache twitchCallCache)
    {
        _twitchCallCache = twitchCallCache;
    }

    // TODO: All Chat that needs to be translated
    public List<ChatDto> Execute(DateTime startTime, DateTime? endTime)
    {
        DateTime date = (DateTime)(endTime != null ? endTime : DateTime.Now);

        IReadOnlyList<ChatDto> allChat = (IReadOnlyList<ChatDto>)_twitchCallCache.GetAllMessagesFromTo(startTime, date, Domain.Enums.CallCacheEnum.CachedMessageData);

        List<ChatDto> translatedChat = new List<ChatDto>();

        foreach (var chat in allChat)
        {
            if (!IsEnglish(chat.Message))
            {
                // TODO: Use https://www.youtube.com/watch?v=04HfJvGSIks for translation
                translatedChat.Add(chat);
            }
        }

        return translatedChat;
    }

    public bool IsEnglish(string message)
    {
        var detector = new LanguageDetector();

        detector.AddAllLanguages();

        var result = detector.Detect(message);

        if (result == "eng")
        {
            return true;
        }
        else
        {
            Console.Write(result);
            return false;
        }
    }
}
