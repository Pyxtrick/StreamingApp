using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Trigger;
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

        if (!commandAndResponse.Active)
        {
            return;
        }
        else if (commandAndResponse.Command.Contains("sstart"))
        {
            // lgic to start Timer
        }
        else if (commandAndResponse.Command.Contains("sstop"))
        {
            // lgic to stop Timer
        }
        else if (commandAndResponse.Command.Contains("sset"))
        {
            // lgic to add time to Timer
        }
    }
}
