using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.Core.Commands.Interfaces;

namespace StreamingApp.Core.Commands;

public class StartInitialise : IStartTwitchApi
{
    private readonly ITwitchInitialise _twitchInitialise;
    //private readonly IVTubeStudioInitialize  _vTubeStudioInitialize;
    //private readonly IObsInitialize  _ObsInitialize;

    public StartInitialise(ITwitchInitialise twitchInitialise)
    {
        _twitchInitialise = twitchInitialise;
    }

    public void Execute()
    {
        _twitchInitialise.StartTwitchBot();
    }
}
