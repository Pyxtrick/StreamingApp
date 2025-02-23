using LanguageDetection;

namespace StreamingApp.Core.Commands.Chat;
public class Translate : ITranslate
{
    /// <summary>
    /// Get Language of Message
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public string? GetLanguage(string message)
    {
        var detector = new LanguageDetector();
        detector.AddAllLanguages();

        var result = detector.Detect(message);

        return result;

        /**if (result == "eng")
        {
            return "eng";
        }
        else
        {
            Console.Write(result);
            return null;
        }**/
    }

    public async Task<String> TranslateMessage(string message)
    {
        // TODO: Implement Translate Logic
        // TODO: Use https://www.youtube.com/watch?v=04HfJvGSIks for translation
        // TODO: OR https://github.com/DeepLcom/deepl-dotnet

        // TODO: Fix this as it is not working reliably
        var t = GetLanguage(message);

        if(t != null)
        {
            return t;
        }

        return "not be able to translate";
    }
}
