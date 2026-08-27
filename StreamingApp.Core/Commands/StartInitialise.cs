using StreamingApp.API.Bluesky.Interfaces;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Youtube;

namespace StreamingApp.Core.Commands;

public class StartInitialise : IStartTwitchApi
{
    private readonly ITwitchInitialise _twitchInitialise;
    private readonly IBlueskyInitialise _blueskyInitialise;
    //private readonly IVTubeStudioInitialize  _vTubeStudioInitialize;
    //private readonly IObsInitialize  _obsInitialize;

    private readonly IYoutubeInitialise _youtubeInitialise;

    public StartInitialise(ITwitchInitialise twitchInitialise, IBlueskyInitialise blueskyInitialise, IYoutubeInitialise youtubeInitialise)
    {
        _twitchInitialise = twitchInitialise;
        _blueskyInitialise = blueskyInitialise;
        _youtubeInitialise = youtubeInitialise;
    }

    public void Execute()
    {
        _twitchInitialise.StartTwitchBot();
        _blueskyInitialise.Initialise();
        //_vTubeStudioInitalize.Start();
        //_obsInitialize.Start();
        _youtubeInitialise.StartYoutubeBot();
    }
}
