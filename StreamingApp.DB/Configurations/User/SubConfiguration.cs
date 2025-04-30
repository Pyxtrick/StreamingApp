using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.InternalDB.User;

namespace StreamingApp.DB.Configurations.User;

public class SubConfiguration : IEntityTypeConfiguration<Sub>
{
    public void Configure(EntityTypeBuilder<Sub> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.CurrentTier).HasConversion<string>();
        builder.Property(a => a.Origin).HasConversion<string>();

        builder.HasOne(a => a.Status).WithMany(status => status.Subs).HasForeignKey(sub => sub.StatusId).IsRequired();
    }
}
