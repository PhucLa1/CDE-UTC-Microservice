
namespace Project.Infrastructure.Data.Configurations
{
    public class FileTodoConfiguration : IEntityTypeConfiguration<FileTodo>
    {
        public void Configure(EntityTypeBuilder<FileTodo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FileTodoId => FileTodoId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FileTodoId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject
            
            builder.HasOne<File>()
              .WithMany()
              .HasForeignKey(o => o.FileId)
              .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Todo>()
              .WithMany()
              .HasForeignKey(o => o.TodoId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
