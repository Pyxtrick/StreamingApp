using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.DB.Configurations.Trigger;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
    }
}
