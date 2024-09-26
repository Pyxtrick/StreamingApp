using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal.User;

using user = StreamingApp.Domain.Entities.Internal.User.User;

namespace StreamingApp.DB.Configurations.User;

public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.UserName).HasMaxLength(100);
        builder.Property(a => a.UserId).HasMaxLength(100);
        builder.Property(a => a.AppAuthEnum).HasConversion<string>();
        builder.HasOne(a => a.User).WithOne(user => user.TwitchDetail).HasForeignKey<user>(user => user.TwitchDetailId).IsRequired();
    }
}
