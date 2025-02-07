using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
        }
    }
}
