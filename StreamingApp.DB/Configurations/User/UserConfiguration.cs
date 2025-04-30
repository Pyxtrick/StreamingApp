using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using user = StreamingApp.Domain.Entities.InternalDB.User.User;

namespace StreamingApp.DB.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<user>
{
    public void Configure(EntityTypeBuilder<user> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(u => u.Id);
        builder.HasMany(u => u.Details).WithOne(userDetail => userDetail.User).HasForeignKey(ud => ud.UserId).IsRequired();
        //builder.HasOne(u => u.Status).WithOne(status => status.User).HasForeignKey<user>(a => a.StatusId).IsRequired();
        builder.HasMany(u => u.Achievements).WithOne(achievements => achievements.User).HasForeignKey(a => a.UserId).IsRequired();
        builder.HasOne(u => u.Ban).WithOne(ban => ban.User).HasForeignKey<user>(user => user.BanId);
    }
}
