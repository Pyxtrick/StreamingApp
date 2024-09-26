using Microsoft.Extensions.Configuration;
using StreamingApp.API.VTubeStudio.Props;
using System.Text;
using System.Text.Json;

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

        var postData = new Item
        {
            Name = "HeadPat",
            Time = 60
        };

        var client = new HttpClient();
        client.BaseAddress = new Uri("URL");

        var json = JsonSerializer.Serialize(postData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync("posts", content).Result;

        if (response.IsSuccessStatusCode)
        {
            /** Only needed if content comes back
            var responseContent = response.Content.ReadAsStringAsync().Result;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var postResponse = JsonSerializer.Deserialize<PostResponse>(responseContent, options);
            **/

            Console.WriteLine("Success: " + response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.IsSuccessStatusCode);
        }
    }

    public void ChangeColour()
    {
        // TODO: change colour of the Character / Model
    }
}
