using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Settings;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Test.TestBuilder.DB;

public static class SettingsBuilder
{
    public static Settings Create(this UnitOfWorkContext unitOfWork)
    {
        Settings settings = new Settings();
        unitOfWork.Add(settings);

        return settings;
    }

    public static Settings WithDefaults(this Settings settings, int id, OriginEnum origin)
    {
        settings.Id = id;
        settings.Origin = origin;
        settings.AllChat = AuthEnum.Undefined;
        settings.MuteAllerts = false;
        settings.PauseAllerts = false;
        settings.IsAdsDisplay = true;
        settings.PauseChatMessages = false;
        settings.ComunityDayActive = false;
        settings.Delay = "*/10 * * * *";
        settings.AllertDelayS = 2;
        settings.TimeOutSeconds = 20;
        settings.TTSSpamAmmount = 5;
        settings.TTSLenghtAmmount = 10;
        settings.TTSAmount = 100;
        settings.Conversion = 1;

        return settings;
    }

    public static Settings WithAmountAndConversion(this Settings settings, double ttsAmount, double conversion)
    {
        settings.TTSAmount = ttsAmount;
        settings.Conversion = conversion;

        return settings;
    }
}
