using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using user = StreamingApp.Domain.Entities.InternalDB.User.User;

namespace StreamingApp.DB.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<user>
{
    public void Configure(EntityTypeBuilder<user> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.TwitchDetail).WithOne(userDetail => userDetail.User).HasForeignKey<user>(a => a.TwitchDetailId).IsRequired();
        // Youtube builder.HasOne(a => a.TwitchDetail).WithOne(userDetail => userDetail.User).HasForeignKey<user>(a => a.TwitchDetailId).IsRequired();
        // Discord builder.HasOne(a => a.TwitchDetail).WithOne(userDetail => userDetail.User).HasForeignKey<user>(a => a.TwitchDetailId).IsRequired();
        // Twitter builder.HasOne(a => a.TwitchDetail).WithOne(userDetail => userDetail.User).HasForeignKey<user>(a => a.TwitchDetailId).IsRequired();
        builder.HasOne(a => a.Status).WithOne(status => status.User).HasForeignKey<user>(a => a.StatusId).IsRequired();
        builder.HasOne(a => a.TwitchAchievements).WithOne(achievements => achievements.User).HasForeignKey<user>(a => a.TwitchAchievementsId).IsRequired();
        // Youtube builder.HasOne(a => a.TwitchAchievements).WithOne(achievements => achievements.User).HasForeignKey<user>(a => a.TwitchAchievementsId).IsRequired();
        builder.HasOne(a => a.Ban).WithOne(ban => ban.User).HasForeignKey<user>(a => a.BanId);
    }
}
