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
using StreamingApp.Domain.Enums.Trigger;
using StreamingApp.Test.TestBuilder.DB;
using StreamingApp.Test.TestBuilder.Dto;
using Xunit;

namespace StreamingApp.Test.Core.Command.Twitch;

public class ManageAlertFixture : DataBaseFixture
{
    [Fact]
    public async Task ExecuteBitAndRedeamAndFollowTest_Bits()
    {
        //Arrange
        MessageAlertDto messageAlertDto = MessageAlertDtoBuilder.Create().WithAlertType(AlertTypeEnum.Bits).WithBitsAndCurrency(100, 0).WithOrigin(OriginEnum.Twitch);

        await using (UnitOfWorkContext unitOfWork = CreateUnitOfWork())
        {
            SettingsBuilder.Create(unitOfWork).WithDefaults(1, OriginEnum.Twitch);

            var alert1 = AlertBuilder.Create(unitOfWork).WithDefaults(1);
            var trigger1 = TriggerBuilder.Create(unitOfWork).WithDefaults(1).WithAmmount(100, false, false);
            var target1 = TargetBuilder.Create(unitOfWork).WithDefaults(1, TargetCondition.Allert).WitTrigger(trigger1);
            var targetData1 = TargetDataBuilder.Create(unitOfWork).WithDefaults(1).WithAlert(alert1).WithTarget(target1);

            var alert2 = AlertBuilder.Create(unitOfWork).WithDefaults(2);
            var trigger2 = TriggerBuilder.Create(unitOfWork).WithDefaults(2).WithAmmount(200, false, false);
            var target2 = TargetBuilder.Create(unitOfWork).WithDefaults(2, TargetCondition.Allert).WitTrigger(trigger2);
            var targetData2 = TargetDataBuilder.Create(unitOfWork).WithDefaults(2).WithAlert(alert2).WithTarget(target2);

            await unitOfWork.SaveChangesAsync();
        }

        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
        Mock<ISubAlertLoong> subAlertLoongMock = new();
        Mock<IHubContext<ChatHub>> clientHubMock = new();
        clientHubMock.Setup(client => client.Clients.All).Returns(new Mock<IClientProxy>().Object);

        Mock<IMovingText> movingTextMock = new();
        Mock<IMessageCheck> messageCheckMock = new();
        messageCheckMock.Setup(messageCheck => messageCheck.ExecuteMessageOnly(It.IsAny<string>())).ReturnsAsync(true);

        Mock<IRaidAlert> raidAlertMock = new();

        var manageAlert = CreateManageAlert(twitchSendRequestMock, subAlertLoongMock, clientHubMock, movingTextMock, messageCheckMock, raidAlertMock);

        //Act
        var resault = await manageAlert.ExecuteBitAndRedeamAndFollow(messageAlertDto);

        //Assert
        clientHubMock.Verify(client => client.Clients.All, Times.Exactly(2));
        Assert.Equal("Given 100 Bits", resault);
    }

    [Fact] //TODO
    public async Task ExecuteBitAndRedeamAndFollowTest_Redeam()
    {
        //Arrange
        MessageAlertDto messageAlertDto = MessageAlertDtoBuilder.Create().WithPointRedeam("PointRedeam").WithAlertType(AlertTypeEnum.PointRedeam).WithOrigin(OriginEnum.Twitch);

        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
        Mock<ISubAlertLoong> subAlertLoongMock = new();
        Mock<IHubContext<ChatHub>> clientHubMock = new();
        clientHubMock.Setup(client => client.Clients.All).Returns(new Mock<IClientProxy>().Object);

        Mock<IMovingText> movingTextMock = new();
        Mock<IMessageCheck> messageCheckMock = new();
        Mock<IRaidAlert> raidAlertMock = new();

        var manageAlert = CreateManageAlert(twitchSendRequestMock, subAlertLoongMock, clientHubMock, movingTextMock, messageCheckMock, raidAlertMock);

        //Act
        var resault = await manageAlert.ExecuteBitAndRedeamAndFollow(messageAlertDto);

        //Assert
        clientHubMock.Verify(client => client.Clients.All, Times.Exactly(1));
        Assert.Equal("Used PointRedeam Point Redeam", resault);
    }

    [Fact] //TODO
    public async Task ExecuteBitAndRedeamAndFollowTest_Follow()
    {
        //Arrange
        MessageAlertDto messageAlertDto = MessageAlertDtoBuilder.Create().WithAlertType(AlertTypeEnum.Follow).WithOrigin(OriginEnum.Twitch);

        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
        Mock<ISubAlertLoong> subAlertLoongMock = new();
        Mock<IHubContext<ChatHub>> clientHubMock = new();
        clientHubMock.Setup(client => client.Clients.All).Returns(new Mock<IClientProxy>().Object);

        Mock<IMovingText> movingTextMock = new();
        Mock<IMessageCheck> messageCheckMock = new();
        Mock<IRaidAlert> raidAlertMock = new();

        var manageAlert = CreateManageAlert(twitchSendRequestMock, subAlertLoongMock, clientHubMock, movingTextMock, messageCheckMock, raidAlertMock);

        //Act
        var resault = await manageAlert.ExecuteBitAndRedeamAndFollow(messageAlertDto);

        //Assert
        clientHubMock.Verify(client => client.Clients.All, Times.Exactly(1));
        Assert.Equal("Fallowed", resault);
    }

    [Fact]
    public async Task ExecuteRaidTest_NoChannelInfo()
    {
        //Arrange
        RaidDto raidDto = new("UserNmae", 1, "Test", false, DateTime.UtcNow);

        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
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
        MessageDto chatMessage = new(false, "Channel", "#ff6b6b", null, "Sub", "Sub", new List<EmoteSetDto>(),
            null, OriginEnum.Twitch,
            new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined }, EffectEnum.none, false, 0, false, "message Id", "userId",
            "UserName", "DisplayName", DateTime.Now);
        SubDto subDto = new("1", "1", "Username", "Dispayname", "Channel", OriginEnum.Twitch, false, 0, 3, TierEnum.Tier1, chatMessage, false, DateTime.Now);

        Mock<ITwitchSendRequest> twitchSendRequestMock = new();
        Mock<ISubAlertLoong> subAlertLoongMock = new();
        Mock<IHubContext<ChatHub>> clientHubMock = new();
        clientHubMock.Setup(client => client.Clients.All).Returns(new Mock<IClientProxy>().Object);

        Mock<IMovingText> movingTextMock = new();
        movingTextMock.Setup(movingText => movingText.ExecuteAlert(30, It.IsAny<string>()));

        Mock<IMessageCheck> messageCheckMock = new();
        Mock<IRaidAlert> raidAlertMock = new();

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
