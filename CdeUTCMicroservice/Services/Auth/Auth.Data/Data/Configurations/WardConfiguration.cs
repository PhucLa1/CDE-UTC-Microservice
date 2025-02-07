using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Data.Configurations
{
    public class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {          
        }
    }
}
