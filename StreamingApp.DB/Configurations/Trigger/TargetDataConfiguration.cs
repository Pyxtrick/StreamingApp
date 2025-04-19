using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Entities.InternalDB.Trigger;

namespace StreamingApp.DB.Configurations.Trigger;
public class TargetDataConfiguration : IEntityTypeConfiguration<TargetData>
{
    public void Configure(EntityTypeBuilder<TargetData> builder)
    {
        builder.HasOne(a => a.Alert).WithMany(emote => emote.TargetData).HasForeignKey(a => a.AlertId).IsRequired();
    }
}
