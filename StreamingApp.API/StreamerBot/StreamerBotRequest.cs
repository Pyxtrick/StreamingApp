using Newtonsoft.Json;
using System.Text;

namespace StreamingApp.API.StreamerBot;
public class StreamerBotRequest
{
    /// <summary>
    /// https://docs.streamer.bot/api/servers/http#getactions-get
    /// </summary>
    /// <returns></returns>
    public async Task<List<Actions>> GetActions()
    {
        HttpResponseMessage response = await new HttpClient().GetAsync("http://127.0.0.1:7474/GetActions");
        string stringResponse = await response.Content.ReadAsStringAsync();
        Result convertedResponse = JsonConvert.DeserializeObject<Result>(stringResponse);

        var values = new Request()
        {
            action = new Actions()
            {
                id = "ef8189f0-227d-4311-a5d0-b780e294014a",
                name = "URL test"
            },
            args = new()
            {
                user = "testUser",
                rawInput = "input value"
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");

        await new HttpClient().PostAsync("http://localhost:7474/DoAction", content);

        return convertedResponse.actions;
    }
}


public class Actions
{
    public string id { get; set; }

    public string name { get; set; }
}

public class Result
{
    public int count { get; set; }
    public List<Actions> actions { get; set; }
}

public class Arg
{
    public string user { get; set; }
    public string rawInput { get; set; }
}

public class Request
{
    public Actions action { get; set; }

    public Arg args { get; set; }
}
