using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.InternalDB.User;

namespace StreamingApp.DB.Configurations.User;

public class SubConfiguration : IEntityTypeConfiguration<Sub>
{
    public void Configure(EntityTypeBuilder<Sub> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.CurrentTier).HasConversion<string>();

        builder.HasOne(a => a.Status).WithOne(status => status.TwitchSub).HasForeignKey<Status>(status => status.TwitchSubId).IsRequired();
        //builder.HasOne(a => a.Status).WithOne(user => user.YouTubeSub).HasForeignKey<Status>(a => a.YouTubeSubId).IsRequired();
    }
}
