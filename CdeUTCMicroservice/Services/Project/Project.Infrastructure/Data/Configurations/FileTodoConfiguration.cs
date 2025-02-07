namespace Project.Infrastructure.Data.Configurations
{
    public class FileTodoConfiguration : IEntityTypeConfiguration<FileTodo>
    {
        public void Configure(EntityTypeBuilder<FileTodo> builder)
        {           
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
