using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.Domain.Entities.Internal;

public class EmotesCondition : EntityBase //TODO Remove
{
    public int Id { get; set; }

    // use the lower ammount condition or exact
    // TODO: money conversion for Youtube as all currencys is avalible on Youtube
    // If not already done by the API / Youtube it self
    public int MoneyAmmount { get; set; }

    // use the lower ammount condition or exact
    public int BitAmmount { get; set; }

    // use the lower ammount condition or exact
    public int ChannelPoints { get; set; }

    public int SubAmmount { get; set; }

    //public List<TierEnum> Tier { get; set; }

    // needs exact ammount to play the emote
    public bool ExactAmmount { get; set; }

    public bool Active { get; set; }

    public bool UseTTS { get; set; }

    public Alert Emote { get; set; }

    public int EmoteId { get; set; }
}
