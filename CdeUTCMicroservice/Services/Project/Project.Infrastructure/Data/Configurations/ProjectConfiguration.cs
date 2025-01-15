
namespace Project.Infrastructure.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Projects>
    {
        public void Configure(EntityTypeBuilder<Projects> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                ProjectId => ProjectId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => ProjectId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject
        }
    }
}
