using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using StreamingApp.API.Utility;

namespace StreamingApp.Test.TestBuilder;

public static class MapperBuilder
{
    public static Mapper CreateMapperConfig()
    {
        return new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new TwitchMappingProfile()), NullLoggerFactory.Instance));
    }
}
