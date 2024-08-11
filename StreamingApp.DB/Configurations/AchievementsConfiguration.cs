using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class AchievementsConfiguration : IEntityTypeConfiguration<Achievements>
{
    public void Configure(EntityTypeBuilder<Achievements> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.User).WithOne(user => user.TwitchAchievements).HasForeignKey<User>(a => a.TwitchAchievementsId).IsRequired();
    }
}
