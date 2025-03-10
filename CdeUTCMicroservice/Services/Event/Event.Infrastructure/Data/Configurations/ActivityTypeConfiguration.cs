﻿using Event.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.Infrastructure.Data.Configurations
{
    public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
          
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Template).HasMaxLength(100).IsRequired();
        }
    }
}
