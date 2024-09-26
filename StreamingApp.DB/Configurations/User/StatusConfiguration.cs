using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal.User;

using user = StreamingApp.Domain.Entities.Internal.User.User;

namespace StreamingApp.DB.Configurations.User;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.UserType).HasConversion<string>();
        builder.HasOne(a => a.User).WithOne(user => user.Status).HasForeignKey<user>(user => user.StatusId).IsRequired();
        builder.HasOne(a => a.TwitchSub).WithOne(sub => sub.Status).HasForeignKey<Status>(a => a.TwitchSubId).IsRequired();
        // Youtube builder.HasOne(a => a.TwitchSub).WithOne(user => user.Status).HasForeignKey<Status>(a => a.TwitchSub).IsRequired();
    }
}
