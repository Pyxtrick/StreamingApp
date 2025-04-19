using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageSubathon
{
    public async Task Execute(CommandAndResponse commandAndResponse)
    {
        string reponse = $"There is currenty no Subathon going on";

        var splitMessage = commandAndResponse.Command.Split(' ');

        if (!commandAndResponse.Active)
        {
            return;
        }
        else if(splitMessage[0].Contains("sstart"))
        {
            // lgic to start Timer
        }
        else if(splitMessage[0].Contains("sstop"))
        {
            // lgic to stop Timer
        }
        else if(splitMessage[0].Contains("sset"))
        {
            // lgic to add time to Timer
        }
    }
}
