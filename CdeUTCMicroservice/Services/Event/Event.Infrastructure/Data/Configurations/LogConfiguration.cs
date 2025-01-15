using Event.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.Infrastructure.Data.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => x.StatusCode);
            builder.Property(x => x.Method).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Data).IsRequired(false);
        }
    }
}
