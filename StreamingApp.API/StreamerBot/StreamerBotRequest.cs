using Newtonsoft.Json;
using System.Text;

namespace StreamingApp.API.StreamerBot;

public class StreamerBotRequest : IStreamerBotRequest
{
    /// <summary>
    /// https://docs.streamer.bot/api/servers/http#getactions-get
    /// </summary>
    /// <returns></returns>
    public async Task<List<Actions>> GetActions()
    {
        HttpResponseMessage response = await new HttpClient().GetAsync("http://localhost:7474/GetActions");
        string stringResponse = await response.Content.ReadAsStringAsync();
        Result convertedResponse = JsonConvert.DeserializeObject<Result>(stringResponse);

        return convertedResponse.actions;
    }

    public async Task DoAction(string id, string name, List<KeyValuePair<string, string>> args)
    {
        var data = TransformData(id, name, args);

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        await new HttpClient().PostAsync("http://localhost:7474/DoAction", content);
    }

    private Request TransformData(string id, string name, List<KeyValuePair<string, string>> args)
    {
        return new Request
        {
            action = new Actions()
            {
                id = id,
                name = name,
            },
            args = new Arg()
            {
                user = args.FirstOrDefault(t => t.Key.Equals("user")).Value,
                rawInput = args.FirstOrDefault(t => t.Key.Equals("rawInput")).Value
            }
        };
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
