using Microsoft.Extensions.Configuration;

namespace StreamingApp.API.VTubeStudio;

public class VTubeStudioApiRequest
{
    private readonly IConfiguration _configuration;

    //private string Port = _configuration["VtubeStudio:Port"];
    //private string IP = _configuration["VtubeStudio:Ip"];

    public VTubeStudioApiRequest(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendRequest()
    {

    }

    public void ChangeLocaton() 
    {
        // TODO: change position of the Character / Model
    }

    public void AddItem()
    {
        // TODO: add Item with a timer
    }

    public void ChangeColour()
    {
        // TODO: change colour of the Character / Model
    }
}
