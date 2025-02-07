using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Data.Configurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {         
        }
    }
}
