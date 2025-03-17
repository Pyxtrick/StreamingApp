using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.DB.Configurations.Trigger;

public class CommandAndResponseConfiguration : IEntityTypeConfiguration<CommandAndResponse>
{
    public void Configure(EntityTypeBuilder<CommandAndResponse> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Command).HasMaxLength(100);
        builder.Property(a => a.Response).HasMaxLength(300);
        builder.Property(a => a.Auth).HasConversion<string>();
        builder.Property(a => a.Category).HasConversion<string>();
        builder.HasOne(a => a.Target).WithOne(target => target.CommandAndResponse).HasForeignKey<Target>(target => target.CommandAndResponseId);
    }
}
