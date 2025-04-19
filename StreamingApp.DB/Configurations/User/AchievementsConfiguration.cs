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
        builder.HasOne(a => a.User).WithOne(user => user.TwitchAchievements).HasForeignKey<user>(user => user.TwitchAchievementsId).IsRequired();
    }
}
