using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace StreamingApp.API.Youtube;

public class YoutubeInitialise : IYoutubeInitialise
{
    private readonly IConfiguration _configuration;

    public YoutubeInitialise(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // <summary>
    /// This example uses the discovery API to list all APIs in the discovery repository.
    /// https://developers.google.com/discovery/v1/using.
    /// <summary>
    /**[STAThread]
    static void Main(string[] args)
    {
        Console.WriteLine("Discovery API Sample");
        Console.WriteLine("====================");
        try
        {
            new Program().Run().Wait();
        }
        catch (AggregateException ex)
        {
            foreach (var e in ex.InnerExceptions)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    **/
    public async Task StartYoutubeBot()
    {
        // Create the service.
        var service = new DiscoveryService(new BaseClientService.Initializer
        {
            ApplicationName = "Youtube Streaming API",
            ApiKey = _configuration["YouTube:ApiKey"],
        });

        // Run the request.
        Console.WriteLine("'Custom Youtube' Executing a list request...");
        var result = await service.Apis.List().ExecuteAsync();

        // Display the results.
        if (result.Items != null)
        {
            foreach (DirectoryList.ItemsData api in result.Items)
            {
                Console.WriteLine("'Custom Youtube'" + api.Id + " - " + api.Title);
            }
        }
    }
}
