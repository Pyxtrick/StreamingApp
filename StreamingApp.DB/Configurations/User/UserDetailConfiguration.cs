using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.InternalDB.User;

using user = StreamingApp.Domain.Entities.InternalDB.User.User;

namespace StreamingApp.DB.Configurations.User;

public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(ud => ud.Id);
        builder.Property(ud => ud.UserName).HasMaxLength(100);
        builder.Property(ud => ud.ExternalUserId).HasMaxLength(100);
        builder.Property(ud => ud.AppAuthEnum).HasConversion<string>();
        builder.Property(ud => ud.Origin).HasConversion<string>();
        builder.HasOne(ud => ud.User).WithMany(user => user.Details).HasForeignKey(ud => ud.UserId).IsRequired();
    }
}
