using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal.Stream;

namespace StreamingApp.DB.Configurations.Stream;

public class GameInfoConfiguration : IEntityTypeConfiguration<GameInfo>
{
    public void Configure(EntityTypeBuilder<GameInfo> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.GameCategory).HasConversion<string>();
    }
}
