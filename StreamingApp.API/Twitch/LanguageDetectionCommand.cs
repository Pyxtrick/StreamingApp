using LanguageDetection;

namespace StreamingApp.API.Twitch;

public class LanguageDetectionCommand
{
    public bool Execute(string message)
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
