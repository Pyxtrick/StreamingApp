using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.DB.Configurations.Trigger;

public class EmotesConfiguration : IEntityTypeConfiguration<Emote>
{
    public void Configure(EntityTypeBuilder<Emote> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
    }
}
