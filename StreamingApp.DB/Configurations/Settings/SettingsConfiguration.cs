using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Setting = StreamingApp.Domain.Entities.Internal.Settings.Settings;

namespace StreamingApp.DB.Configurations.Settings;

public class SettingsConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Origin).HasConversion<string>();
        builder.Property(a => a.AllChat).HasConversion<string>();
    }
}
