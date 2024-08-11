﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.DB.Configurations;

public class StreamGameConfiguration : IEntityTypeConfiguration<StreamGame>
{
    public void Configure(EntityTypeBuilder<StreamGame> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.StreamId);
        builder.HasKey(a => a.GameCategoryId);
        builder.HasOne(a => a.Stream).WithMany(user => user.GameCategories).HasForeignKey(a => a.StreamId).IsRequired();
        builder.HasOne(a => a.GameCategory).WithMany(user => user.GameCategories).HasForeignKey(a => a.GameCategoryId).IsRequired();
    }
}
