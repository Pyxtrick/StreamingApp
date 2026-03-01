using Microsoft.AspNetCore.SignalR;
using Moq;
using StreamingApp.API.Interfaces;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Commands.Twitch;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Queries.Alerts.Interfaces;
using StreamingApp.Core.Queries.Logic.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.APIs;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;
using StreamingApp.Test.TestBuilder;
using Xunit;

namespace StreamingApp.Test.Core.Command.Twitch;

public class ManageAlertFixture : DataBaseFixture
{
    [Fact] //TODO
    public async Task ExecuteBitAndRedeamAndFollowTest()
    {

    }

    [Fact]
    public async Task ExecuteRaidTest_NoChannelInfo()
    {
        //Arrange
        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
        Mock<ISubAlertLoong> subAlertLoongMock = new();
        Mock<IHubContext<ChatHub>> clientHubMock = new();
        clientHubMock.Setup(client => client.Clients.All).Returns(new Mock<IClientProxy>().Object);
        
        Mock<IMovingText> movingTextMock = new();
        Mock<IMessageCheck> messageCheckMock = new();
        Mock<IRaidAlert> raidAlertMock = new();

        RaidDto raidDto = new("UserNmae", 1, "Test", false, DateTime.UtcNow);

        var manageAlert = CreateManageAlert(twitchSendRequestMock, subAlertLoongMock, clientHubMock, movingTextMock, messageCheckMock, raidAlertMock);

        //Act
        await manageAlert.ExecuteRaid(raidDto);

        //Assert
        clientHubMock.Verify(client => client.Clients.All, Times.Exactly(2));
    }

    [Fact]
    public async Task ExecuteRaidTest_WithChannelInfo()
    {
        //Arrange
        RaidDto raidDto = new("UserNmae", 1, "Test", false, DateTime.UtcNow);
        ChannelInfo channelInfo = new ChannelInfo(){ GameId = "111",GameName = "TestGame", Title = "Title" };

        await using (UnitOfWorkContext unitOfWork = CreateUnitOfWork())
        {
            CommandAndResponseBuilder.Create(unitOfWork).WithDefaults().WithCommandResponse("so", "");

            await unitOfWork.SaveChangesAsync();
        }

        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
        twitchSendRequestMock.Setup(request => request.GetChannelInfo(raidDto.UserName, false)).ReturnsAsync(channelInfo);

        Mock<ISubAlertLoong> subAlertLoongMock = new();
        Mock<IHubContext<ChatHub>> clientHubMock = new();
        clientHubMock.Setup(client => client.Clients.All).Returns(new Mock<IClientProxy>().Object);


        Mock<IMovingText> movingTextMock = new();
        Mock<IMessageCheck> messageCheckMock = new();
        Mock<IRaidAlert> raidAlertMock = new();

        var manageAlert = CreateManageAlert(twitchSendRequestMock, subAlertLoongMock, clientHubMock, movingTextMock, messageCheckMock, raidAlertMock);

        //Act
        await manageAlert.ExecuteRaid(raidDto);

        //Assert
        clientHubMock.Verify(client => client.Clients.All, Times.Exactly(3));
    }

    [Fact]
    public async Task ExecuteSubTest()
    {
        //Arrange
        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
        Mock<ISubAlertLoong> subAlertLoongMock = new();
        Mock<IHubContext<ChatHub>> clientHubMock = new();
        clientHubMock.Setup(client => client.Clients.All).Returns(new Mock<IClientProxy>().Object);

        Mock<IMovingText> movingTextMock = new();
        movingTextMock.Setup(movingText => movingText.ExecuteAlert(30, It.IsAny<string>()));

        Mock<IMessageCheck> messageCheckMock = new();
        Mock<IRaidAlert> raidAlertMock = new();

        MessageDto chatMessage = new(false, "Channel", "#ff6b6b", null, "Sub", "Sub", new List<EmoteSetDto>(),
            null, OriginEnum.Twitch,
            new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined }, EffectEnum.none, false, 0, false, "message Id", "userId",
            "UserName", "DisplayName", DateTime.Now);
        SubDto subDto = new("1", "1", "Username", "Dispayname", "Channel", OriginEnum.Twitch, false, 0, 3, TierEnum.Tier1, chatMessage, false, DateTime.Now);

        var manageAlert = CreateManageAlert(twitchSendRequestMock, subAlertLoongMock, clientHubMock, movingTextMock, messageCheckMock, raidAlertMock);

        //Act
        await manageAlert.ExecuteSub(subDto);

        //Assert
        clientHubMock.Verify(client => client.Clients.All, Times.Once);

    }

    private IManageAlert CreateManageAlert(Mock<ITwitchSendRequest> twitchSendRequestMock, Mock<ISubAlertLoong> subAlertLoongMock, Mock<IHubContext<ChatHub>> clientHubMock, Mock<IMovingText> movingTextMock, Mock<IMessageCheck> messageCheckMock, Mock<IRaidAlert> raidAlertMock)
    {
        return new ManageAlert(CreateUnitOfWork(), twitchSendRequestMock.Object, subAlertLoongMock.Object, clientHubMock.Object, movingTextMock.Object, messageCheckMock.Object, raidAlertMock.Object);
    }
}
