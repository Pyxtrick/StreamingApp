using StreamingApp.API.Bluesky;
using StreamingApp.API.Twitch.Interfaces;

namespace StreamingApp.Core.Commands;

public class StartInitialise : IStartTwitchApi
{
    private readonly ITwitchInitialise _twitchInitialise;
    private readonly IBlueskyInitialise _blueskyInitialise;
    //private readonly IVTubeStudioInitialize  _vTubeStudioInitialize;
    //private readonly IObsInitialize  _obsInitialize;

    public StartInitialise(ITwitchInitialise twitchInitialise, IBlueskyInitialise blueskyInitialise)
    {
        _twitchInitialise = twitchInitialise;
        _blueskyInitialise = blueskyInitialise;
    }

    public void Execute()
    {
        _twitchInitialise.StartTwitchBot();
        _blueskyInitialise.Initialise();
        //_vTubeStudioInitalize.Start();
        //_obsInitialize.Start();
    }
}
