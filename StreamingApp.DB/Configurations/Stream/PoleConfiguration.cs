﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.InternalDB.Stream;

namespace StreamingApp.DB.Configurations.Stream;

public class PoleConfiguration : IEntityTypeConfiguration<Pole>
{
    public void Configure(EntityTypeBuilder<Pole> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.Id);
    }
}
