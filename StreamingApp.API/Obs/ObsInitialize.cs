using Microsoft.Extensions.Configuration;

namespace StreamingApp.API.Obs;

public class ObsInitialize
{
    private readonly IConfiguration _configuration;

    //private string Port = _configuration["Obs:Port")];
    //private string IP = _configuration["Obs:Ip"];
    //private string PW = _configuration["Obs:Password"];

    public ObsInitialize(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // TODO: OBS controls
    // Simular to file:///E:/Saves/Streaming/obs-web-gh-pages/index.html
    public async Task InitializeWebSocketAsync()
    {

    }
}
