using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.UserType).HasConversion<string>();
        builder.HasOne(a => a.User).WithOne(user => user.Status).HasForeignKey<User>(a => a.StatusId).IsRequired();
        builder.HasOne(a => a.TwitchSub).WithOne(user => user.Status).HasForeignKey<Status>(a => a.TwitchSubId).IsRequired();
        // Youtube builder.HasOne(a => a.TwitchSub).WithOne(user => user.Status).HasForeignKey<Status>(a => a.TwitchSub).IsRequired();
    }
}
