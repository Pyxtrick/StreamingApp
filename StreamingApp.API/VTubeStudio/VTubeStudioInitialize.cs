using Microsoft.Extensions.Configuration;
using VTS.Core;

namespace StreamingApp.API.VTubeStudio;

public class VTubeStudioInitialize : CoreVTSPlugin
{
    IVTSLogger _logger;

    CoreVTSPlugin _plugin;

    private readonly IConfiguration _configuration;

    public VTubeStudioInitialize(IVTSLogger logger, IConfiguration configuration, int updateIntervalMs, string pluginName, string pluginAuthor, string pluginIcon) : base(logger, updateIntervalMs, pluginName, pluginAuthor, pluginIcon)
    {
        _logger = logger;
        _configuration = configuration;
    }

    /**
     * ip Default 192.168.111.106 Server PC
     * port Default 8001
     */
    public async Task InitializeWebSocketAsync(string? ip = null, int port = 0)
    {
        var websocket = new WebSocketNetCoreImpl(_logger);
        var jsonUtility = new NewtonsoftJsonUtilityImpl();
        var tokenStorage = new TokenStorageImpl("");

        _plugin = new CoreVTSPlugin(_logger, 100, "My first plugin", "My Name", "");

        SetIPAddress(ip != null ? ip : _configuration["VtubeStudio:Ip"]);
        SetPort(port != 0 ? port : int.Parse(_configuration["VtubeStudio:Port"]));

        try
        {
            await _plugin.InitializeAsync(websocket, jsonUtility, tokenStorage, () => _logger.LogWarning("Disconnected!"));
            _logger.Log("Connected!");

            var apiState = await _plugin.GetAPIStateAsync();
            _logger.Log("Using VTubeStudio " + apiState.data.vTubeStudioVersion);

            var currentModel = await _plugin.GetCurrentModelAsync();
            _logger.Log("The current model is: " + currentModel.data.modelName);
        }
        catch (VTSException e)
        {
            _logger.LogError(e);
        }
    }

    public void DisconnectWebSocket()
    {
        _plugin.Disconnect();
    }
}
