using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(30);
            builder.Property(x => x.WorkPhoneNumber).HasMaxLength(12);
            builder.Property(x => x.MobilePhoneNumber).HasMaxLength(12);
            builder.HasOne<JobTitle>()
                .WithMany()
                .HasForeignKey(o => o.JobTitleId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<Language>()
                .WithMany()
                .HasForeignKey(o => o.LanguageId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<City>()
                .WithMany()
                .HasForeignKey(o => o.CityId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
