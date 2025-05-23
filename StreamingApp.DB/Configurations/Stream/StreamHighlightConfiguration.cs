﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.InternalDB.Stream;

namespace StreamingApp.DB.Configurations.Stream;

public class StreamHighlightConfiguration : IEntityTypeConfiguration<StreamHighlight>
{
    public void Configure(EntityTypeBuilder<StreamHighlight> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.Stream).WithMany(stream => stream.StreamHighlights).HasForeignKey(a => a.StreamId).IsRequired();
    }
}
