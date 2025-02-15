
namespace StreamingApp.Core.Commands.Chat;

public interface ITranslate
{
    string? GetLanguage(string message);
    Task<string> TranslateMessage(string message);
}