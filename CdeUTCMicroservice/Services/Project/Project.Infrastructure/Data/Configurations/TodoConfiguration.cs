
namespace Project.Infrastructure.Data.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                TodoId => TodoId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => TodoId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

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
