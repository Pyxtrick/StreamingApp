using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class StreamHighlightConfiguration : IEntityTypeConfiguration<StreamHighlight>
{
    public void Configure(EntityTypeBuilder<StreamHighlight> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.Stream).WithMany(user => user.StreamHighlights).HasForeignKey(a => a.StreamId).IsRequired();
    }
}
