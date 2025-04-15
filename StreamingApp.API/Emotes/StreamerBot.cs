using Newtonsoft.Json;
using StreamingApp.Domain.Entities.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.API.Emotes;
public  class StreamerBot
{
    public async Task<List<String>> Execute()
    {
        /**HttpResponseMessage response = await new HttpClient().GetAsync($"");
        string stringResponse = await response.Content.ReadAsStringAsync();
        List<string> convertedResponse = JsonConvert.DeserializeObject<List<_7TVEmoteList>>(stringResponse);
        **/

        return new List<string>();
    }
}
