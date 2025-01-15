
namespace Project.Infrastructure.Data.Configurations
{
    public class ViewTodoConfiguration : IEntityTypeConfiguration<ViewTodo>
    {
        public void Configure(EntityTypeBuilder<ViewTodo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                ViewTodoId => ViewTodoId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => ViewTodoId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<View>()
              .WithMany()
              .HasForeignKey(o => o.ViewId)
              .IsRequired()
              .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Todo>()
              .WithMany()
              .HasForeignKey(o => o.TodoId)
              .IsRequired()
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
