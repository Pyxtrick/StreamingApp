namespace StreamingApp.API.Twitch.Interfaces;

public interface ITwitchInitialise
{
    void CloseConntection(object sender, EventArgs e);
    void InitializeWebServer();
    void StartTwitchBot();
}