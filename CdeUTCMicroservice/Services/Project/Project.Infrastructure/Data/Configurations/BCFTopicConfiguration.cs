
namespace Project.Infrastructure.Data.Configurations
{
    public class BCFTopicConfiguration : IEntityTypeConfiguration<BCFTopic>
    {
        public void Configure(EntityTypeBuilder<BCFTopic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                BCFTopicId => BCFTopicId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => BCFTopicId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject


            builder.HasOne(x => x.Project)
                .WithMany(x => x.BCFTopics)
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.ComplexProperty(
                o => o.Characteristic, //1. Thuộc tính phức tạp 
                nameBuilder => //2. Cấu hình chi tiết cho thuộc tính
                {
                    nameBuilder.Property(n => n.TypeId);
                    nameBuilder.Property(n => n.StatusId);
                    nameBuilder.Property(n => n.PriorityId);

                });
        }
    }
}
