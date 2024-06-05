using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class CommandAndResponseConfiguration : IEntityTypeConfiguration<CommandAndResponse>
{
    public void Configure(EntityTypeBuilder<CommandAndResponse> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Command).HasMaxLength(100);
        builder.Property(a => a.Response).HasMaxLength(200);
    }
}
