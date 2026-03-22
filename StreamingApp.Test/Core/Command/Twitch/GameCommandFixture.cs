using Moq;
using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Twitch;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;
using StreamingApp.Test.TestBuilder.DB;
using Xunit;

namespace StreamingApp.Test.Core.Command.Twitch;

public class GameCommandFixture : DataBaseFixture
{
    [Fact]
    public async Task ExecuteGameInfo()
    {
        CommandAndResponse commandAndResponse = new() { Category = CategoryEnum.Game, Command = "gameinfo", Response = "game Info" };
        ChannelInfo channelInfo = new ChannelInfo() { GameId = "1111", GameName = "Test Game", Title = "" };

        await using (UnitOfWorkContext unitOfWork = CreateUnitOfWork())
        {
            var gameInfo = GameInfoBuilder.Create(unitOfWork).WithDefaults(1).WithData("TestGame", "1111", "Test Game Text", GameCategoryEnum.Info);
            var stream = StreamBuilder.Create(unitOfWork).WithDefaults(1).WithTitleAndVodUrl("", "");
            StreamGameBuilder.Create(unitOfWork).WithDefaults(1).WithGameCategory(gameInfo).WithStream(stream);

            await unitOfWork.SaveChangesAsync();
        }

        Mock<ITwitchSendRequest> sendRequest = new();
        sendRequest.Setup(sendRequest => sendRequest.GetChannelInfo(null, true)).ReturnsAsync(channelInfo);

        var gameCommand = new GameCommand(sendRequest.Object, CreateUnitOfWork());

        var resault = await gameCommand.Execute(commandAndResponse);

        Assert.Equal("Test Game Text", resault);
    }

    [Fact]
    public async Task ExecuteModpack()
    {
        CommandAndResponse commandAndResponse = new() { Category = CategoryEnum.Game, Command = "modpack", Response = "" };
        ChannelInfo channelInfo = new ChannelInfo() { GameId = "1111", GameName = "Test Game", Title = "" };

        await using (UnitOfWorkContext unitOfWork = CreateUnitOfWork())
        {
            var gameInfo = GameInfoBuilder.Create(unitOfWork).WithDefaults(1).WithData("TestGame", "1111", "Test Game Text", GameCategoryEnum.ModPack);
            var stream = StreamBuilder.Create(unitOfWork).WithDefaults(1).WithTitleAndVodUrl("", "");
            StreamGameBuilder.Create(unitOfWork).WithDefaults(1).WithGameCategory(gameInfo).WithStream(stream);

            await unitOfWork.SaveChangesAsync();
        }

        Mock<ITwitchSendRequest> sendRequest = new();
        sendRequest.Setup(sendRequest => sendRequest.GetChannelInfo(null, true)).ReturnsAsync(channelInfo);

        var gameCommand = new GameCommand(sendRequest.Object, CreateUnitOfWork());

        var resault = await gameCommand.Execute(commandAndResponse);

        Assert.Equal("Test Game Text", resault);
    }

    [Fact]
    public async Task ExecuteGameprogress()
    {
        CommandAndResponse commandAndResponse = new() { Category = CategoryEnum.Game, Command = "gameprogress", Response = "" };
        ChannelInfo channelInfo = new ChannelInfo() { GameId = "1111", GameName = "Test Game", Title = "" };

        await using (UnitOfWorkContext unitOfWork = CreateUnitOfWork())
        {
            var gameInfo = GameInfoBuilder.Create(unitOfWork).WithDefaults(1).WithData("TestGame", "1111", "Test Game Text", GameCategoryEnum.Progress);
            var stream = StreamBuilder.Create(unitOfWork).WithDefaults(1).WithTitleAndVodUrl("", "");
            StreamGameBuilder.Create(unitOfWork).WithDefaults(1).WithGameCategory(gameInfo).WithStream(stream);

            await unitOfWork.SaveChangesAsync();
        }

        Mock<ITwitchSendRequest> sendRequest = new();
        sendRequest.Setup(sendRequest => sendRequest.GetChannelInfo(null, true)).ReturnsAsync(channelInfo);

        var gameCommand = new GameCommand(sendRequest.Object, CreateUnitOfWork());

        var resault = await gameCommand.Execute(commandAndResponse);

        Assert.Equal("Test Game Text", resault);
    }
}
