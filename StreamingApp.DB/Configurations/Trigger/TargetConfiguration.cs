using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.DB.Configurations.Trigger;

public class TargetConfiguration : IEntityTypeConfiguration<Target>
{
    public void Configure(EntityTypeBuilder<Target> builder)
    {
        builder.HasOne(a => a.Trigger).WithMany(trigger => trigger.Targets).HasForeignKey(a => a.TriggerId).IsRequired();
        builder.HasOne(a => a.TargetData).WithOne(targetData => targetData.Target).HasForeignKey<TargetData>(targetData => targetData.TargetId).IsRequired();

        builder.Property(a => a.TargetCondition).HasConversion<string>();
    }
}
