namespace Project.Infrastructure.Data.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {          
            builder.Property(f => f.Size)
                .HasPrecision(18, 4);

            builder.HasIndex(o => o.FullPath).IsUnique();
        }
    }
}
