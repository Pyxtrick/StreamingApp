using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class SubConfiguration : IEntityTypeConfiguration<Sub>
{
    public void Configure(EntityTypeBuilder<Sub> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.CurrentTier).HasConversion<string>();

        builder.HasOne(a => a.Status).WithOne(user => user.TwitchSub).HasForeignKey<Status>(a => a.TwitchSubId).IsRequired();
    }
}
