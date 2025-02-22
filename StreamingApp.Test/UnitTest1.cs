namespace StreamingApp.Test;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;
using StreamingApp.Tests;
using System;
using Xunit;

public class Tests : DataBaseFixture
{
    [Fact]
    public void Test1()
    {
        // Arrange
        DateTime starTime = new(2022, 2, 14);

        using (UnitOfWorkContext unitOfWork = CreateUnitOfWork())
        {
            /**User user = UserBuilder.Create(unitOfWork).WithDefaults();

            ActivityBuilder.Create(unitOfWork).WithDefaults().WithUser(user);

            await unitOfWork.SaveChangesAsync();
            **/
        }

        var auths = new List<AuthEnum> { AuthEnum.Mod, AuthEnum.Streamer };

        CommandDto commandDto = new("1", "user1", "user", "!live", auths, DateTime.UtcNow, ChatOriginEnum.Twtich);

        //IManageCommands createOrReplaceActivitiesCommand = CreateCommand();

        // Act
        //await createOrReplaceActivitiesCommand.Execute(commandDto);

        // Assert

    }

    [Fact]
    public void Test2()
    {
        // Arrange
        string message1 = "!live"; // UTC +2
        string message = "!live  America"; // UTC -7
        string message3 = "!live  Japan"; // UTC +9

        var splitMessage = message.Split(' ');


        // Act
        var localTime = DateTime.Now.Date + new TimeSpan(17, 30, 0);
        string reponse = "undefined";


        if (splitMessage.Length > 1)
        {
            try
            {
                var result = localTime.AddDays(((int)DayOfWeek.Friday - (int)localTime.DayOfWeek + 7) % 7);

                var timeZone = TimeZoneInfo.ConvertTime(result, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(splitMessage[2]));

                var offset = (timeZone - result.ToUniversalTime()).Hours;

                var offsetText = offset > 0 ? $"+{offset}" : $"{offset}";

                reponse = $"Stream will be live on {timeZone.DayOfWeek} and {timeZone.AddDays(1).DayOfWeek} at {timeZone.TimeOfDay.ToString()} {splitMessage[2]} / (UTC {offsetText})";
            }
            catch (Exception)
            {
                reponse = "Use the TZ identifier for this command !live https://en.wikipedia.org/wiki/List_of_tz_database_time_zones";
            }
        }
        else
        {
            var result = localTime.AddDays(((int)DayOfWeek.Friday - (int)localTime.DayOfWeek + 7) % 7);

            reponse = $"{result.DayOfWeek} and {result.AddDays(1).DayOfWeek} at {localTime.TimeOfDay.ToString()} {result.Kind} / {result.ToUniversalTime().TimeOfDay} {result.ToUniversalTime().Kind}";
        }

        // Assert
        Assert.Equal("", reponse);
    }

    /** for Mocking classes
    private IManageCommands CreateCommand()
    {
        Mock<ManageCommands> loggerMock = new();

        return new ManageCommands(CreateUnitOfWork(), );
    }**/

    //public ManageCommands(UnitOfWorkContext unitOfWork, ICheck checkAuth, IQueueCommand queueCommand, IGameCommand gameCommand)
}