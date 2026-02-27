using Microsoft.Extensions.Configuration;

namespace StreamingApp.API.Fluxer;

public class FluxerInitialise
{
    private readonly IConfiguration _configuration;

    public async Task StartFluxerBot()
    {
        var t = "https://web.fluxer.app/oauth2/authorize?client_id=1475074720098414693&scope=bot";

        var secret = _configuration["Fluxer:secret"];
        var tocken = _configuration["Fluxer:tocken"];
    }
}
