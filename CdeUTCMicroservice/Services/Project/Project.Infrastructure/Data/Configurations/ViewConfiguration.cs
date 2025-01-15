
namespace Project.Infrastructure.Data.Configurations
{
    public class ViewConfiguration : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                ViewId => ViewId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => ViewId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.Property(x => x.FileId).HasConversion(
                FileId => FileId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FileId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject
        }
    }
}
