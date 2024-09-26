using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using stream = StreamingApp.Domain.Entities.Internal.Stream.Stream;

namespace StreamingApp.DB.Configurations.Stream;

public class StreamConfiguration : IEntityTypeConfiguration<stream>
{
    public void Configure(EntityTypeBuilder<stream> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
    }
}
