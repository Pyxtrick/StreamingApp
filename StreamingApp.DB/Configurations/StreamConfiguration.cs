using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class StreamConfiguration : IEntityTypeConfiguration<Domain.Entities.Internal.Stream>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Internal.Stream> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
    }
}
