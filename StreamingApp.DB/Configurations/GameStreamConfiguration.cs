using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class GameStreamConfiguration : IEntityTypeConfiguration<GameStream>
{
    public void Configure(EntityTypeBuilder<GameStream> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.StreamId);
        builder.HasKey(a => a.GameCategoryId);
    }
}
