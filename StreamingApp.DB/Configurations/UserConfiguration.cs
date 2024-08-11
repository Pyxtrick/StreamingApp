using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.TwitchDetail).WithOne(user => user.User).HasForeignKey<User>(a => a.TwitchDetailId).IsRequired();
        // Youtube builder.HasOne(a => a.TwitchDetail).WithOne(user => user.User).HasForeignKey<User>(a => a.TwitchDetailId).IsRequired();
        // Discord builder.HasOne(a => a.TwitchDetail).WithOne(user => user.User).HasForeignKey<User>(a => a.TwitchDetailId).IsRequired();
        // Twitter builder.HasOne(a => a.TwitchDetail).WithOne(user => user.User).HasForeignKey<User>(a => a.TwitchDetailId).IsRequired();
        builder.HasOne(a => a.Status).WithOne(user => user.User).HasForeignKey<User>(a => a.StatusId).IsRequired();
        builder.HasOne(a => a.TwitchAchievements).WithOne(user => user.User).HasForeignKey<User>(a => a.TwitchAchievementsId).IsRequired();
        // Youtube builder.HasOne(a => a.TwitchAchievements).WithOne(user => user.User).HasForeignKey<User>(a => a.TwitchAchievementsId).IsRequired();
        builder.HasOne(a => a.Ban).WithOne(user => user.User).HasForeignKey<User>(a => a.BanId);
    }
}
