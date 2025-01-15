using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Event.Core.Entities;

namespace Event.Infrastructure.Data.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasKey(x => x.Action);
            builder.Property(x => x.Action).HasMaxLength(255);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.ProjectId).IsRequired();
        }
    }
}
