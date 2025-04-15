
namespace StreamingApp.API.StreamerBot;

public interface IStreamerBotRequest
{
    Task DoAction(string id, string name, List<KeyValuePair<string, string>> args);
    Task<List<Actions>> GetActions();
}