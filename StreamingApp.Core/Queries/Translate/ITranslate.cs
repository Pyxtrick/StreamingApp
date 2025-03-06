namespace StreamingApp.Core.Queries.Translate;

public interface ITranslate
{
    string? GetLanguage(string message);
    Task<string> TranslateMessage(string message);
}