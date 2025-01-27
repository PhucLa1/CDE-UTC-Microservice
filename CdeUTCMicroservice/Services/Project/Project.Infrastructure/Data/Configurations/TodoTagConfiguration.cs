
namespace Project.Infrastructure.Data.Configurations
{
    public class TodoTagConfiguration : IEntityTypeConfiguration<TodoTag>
    {
        public void Configure(EntityTypeBuilder<TodoTag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                TodoTagId => TodoTagId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => TodoTagId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<Todo>()
                .WithMany()
                .HasForeignKey(o => o.TodoId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<Tag>()
                .WithMany()
                .HasForeignKey(o => o.TagId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
