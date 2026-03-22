using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using trigger = StreamingApp.Domain.Entities.InternalDB.Trigger.Trigger;

namespace StreamingApp.DB.Configurations.Trigger;

internal class TriggerConfiguration : IEntityTypeConfiguration<trigger>
{
    public void Configure(EntityTypeBuilder<trigger> builder)
    {
        builder.Property(a => a.TriggerCondition).HasConversion<string>();
        builder.Property(a => a.Auth).HasConversion<string>();
    }
}
