using Event.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Event.Infrastructure.Data.Configurations
{
    public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(25);
            builder.Property(x => x.TimeSend).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Template).HasMaxLength(25).IsRequired();
        }
    }
}
