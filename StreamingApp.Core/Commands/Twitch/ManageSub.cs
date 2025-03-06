using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageSub
{
    private readonly IManageFile _manageFile;

    public async Task Execute(SubDto sub)
    {
        // Sub
        // Prime Sub
        // Gifted 1 Sub / Gifted X Subs

    }


}
