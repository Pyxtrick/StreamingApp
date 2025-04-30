using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.InternalDB.User;

using user = StreamingApp.Domain.Entities.InternalDB.User.User;

namespace StreamingApp.DB.Configurations.User;

public class AchievementsConfiguration : IEntityTypeConfiguration<Achievements>
{
    public void Configure(EntityTypeBuilder<Achievements> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Origin).HasConversion<string>();
        builder.HasOne(a => a.User).WithMany(user => user.Achievements).HasForeignKey(a => a.UserId).IsRequired();
    }
}
