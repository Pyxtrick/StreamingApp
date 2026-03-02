using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Test.TestBuilder.Dto;

public static class MessageAlertDtoBuilder
{
    public static MessageAlertDto Create()
    {
        return new MessageAlertDto("messageId", "Channel", "UserId", "UserName", "DisplayName", "colorHex", "Message", "", null, 0, 0, new(), new(), OriginEnum.Twitch, AlertTypeEnum.Bits, new List<AuthEnum>() { AuthEnum.Undefined }, false, false, DateTime.UtcNow);
    }

    public static MessageAlertDto WithPointRedeam(this MessageAlertDto messageAlertDto, string pointRediam)
    {
        messageAlertDto.PointRediam = pointRediam;
        return messageAlertDto;
    }

    public static MessageAlertDto WithBitsAndCurrency(this MessageAlertDto messageAlertDto, int bits, double currency)
    {
        messageAlertDto.Bits = bits;
        messageAlertDto.Currency = currency;
        return messageAlertDto;
    }

    public static MessageAlertDto WithOrigin(this MessageAlertDto messageAlertDto, OriginEnum origin)
    {
        messageAlertDto.Origin = origin;
        return messageAlertDto;
    }

    public static MessageAlertDto WithAlertType(this MessageAlertDto messageAlertDto, AlertTypeEnum alertType)
    {
        messageAlertDto.AlertType = alertType;
        return messageAlertDto;
    }
}
