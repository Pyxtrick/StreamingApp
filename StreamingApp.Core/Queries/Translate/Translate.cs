using LanguageDetection;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web;

namespace StreamingApp.Core.Queries.Translate;
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

        return detector.Detect(message);
    }

    public async Task<string> TranslateMessage(string message)
    {
        // TODO: Implement Translate Logic
        // TODO: Use https://www.youtube.com/watch?v=04HfJvGSIks for translation
        // TODO: OR https://github.com/DeepLcom/deepl-dotnet

        var detector = new LanguageDetector();
        detector.AddAllLanguages();

        var result = detector.Detect(message);

        if (result == "eng")
        {
            return "eng";
        }
        else
        {
            var translated = TranslateWGoogleapis(message);
            Console.Write(translated);
            return translated;
        }
    }

    public String TranslateWGoogleapis(string text, string fromLanguage = "auto", string toLanguage = "en")
    {
        var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(text)}";

        var webClient = new WebClient
        {
            Encoding = System.Text.Encoding.UTF8
        };
        string result = webClient.DownloadString(url);

        JArray array = JArray.Parse(result);

        // All translation info is contained in the first item of the JArray
        JArray translationItems = array[0] as JArray;

        string translation = "";

        // translationItem is also a JArray, with each item being a new sentence
        foreach (JArray item in translationItems)
        {
            // Translated sentence is the first item of the translationItem
            string translationLineString = item[0].ToString();

            translation += $" {translationLineString}";
        }

        if (translation.Length > 1)
        {
            translation = translation.Substring(1);
        }
        ;

        return translation;
    }
}
