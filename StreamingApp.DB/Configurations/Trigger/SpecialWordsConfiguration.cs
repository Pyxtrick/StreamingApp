using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.DB.Configurations.Trigger;

public class SpecialWordsConfiguration : IEntityTypeConfiguration<SpecialWords>
{
    public void Configure(EntityTypeBuilder<SpecialWords> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Type).HasConversion<string>();
    }
}
