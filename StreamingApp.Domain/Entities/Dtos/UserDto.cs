using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Internal;

public class UserDto
{
    public int Id { get; set; }

    public string UserName { get; set; }

    #region UserDetail
    public string Url { get; set; }
    #endregion

    #region Status
    public UserTypeEnum UserType { get; set; }
    #endregion

    #region Achievements
    public int GiftedSubsCount { get; set; }

    public int GiftedBitsCount { get; set; }

    public int GiftedDonationCount { get; set; }

    public int WachedStreams { get; set; }
    #endregion
}
