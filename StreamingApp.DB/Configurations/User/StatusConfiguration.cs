using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.InternalDB.User;

using user = StreamingApp.Domain.Entities.InternalDB.User.User;

namespace StreamingApp.DB.Configurations.User;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.UserType).HasConversion<string>();
        builder.HasOne(a => a.User).WithOne(user => user.Status).HasForeignKey<Status>(s => s.UserId).IsRequired();
        builder.HasMany(a => a.Subs).WithOne(sub => sub.Status).HasForeignKey(sub => sub.StatusId).IsRequired();
    }
}
