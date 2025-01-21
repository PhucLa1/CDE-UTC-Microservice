using Event.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Event.Infrastructure.Data.Configurations
{
    public class ActivityTypeParentConfiguration : IEntityTypeConfiguration<ActivityTypeParent>
    {
        public void Configure(EntityTypeBuilder<ActivityTypeParent> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.IconImageUrl).HasMaxLength(50).IsRequired();
        }
    }
}
