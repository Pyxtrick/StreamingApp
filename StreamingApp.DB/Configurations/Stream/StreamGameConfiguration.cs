﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingApp.Domain.Entities.InternalDB.Stream;

namespace StreamingApp.DB.Configurations.Stream;

public class StreamGameConfiguration : IEntityTypeConfiguration<StreamGame>
{
    public void Configure(EntityTypeBuilder<StreamGame> builder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(builder, nameof(builder));

        builder.HasKey(a => a.StreamGameId);
        builder.HasOne(a => a.Stream).WithMany(stream => stream.GameCategories).HasForeignKey(a => a.StreamId).IsRequired();
        builder.HasOne(a => a.GameCategory).WithMany(gameInfo => gameInfo.GameCategories).HasForeignKey(a => a.GameCategoryId).IsRequired();
    }
}
