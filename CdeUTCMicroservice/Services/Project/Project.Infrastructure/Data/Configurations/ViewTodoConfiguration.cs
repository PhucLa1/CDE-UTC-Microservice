namespace Project.Infrastructure.Data.Configurations
{
    public class ViewTodoConfiguration : IEntityTypeConfiguration<ViewTodo>
    {
        public void Configure(EntityTypeBuilder<ViewTodo> builder)
        {
            builder.HasOne<View>()
              .WithMany()
              .HasForeignKey(o => o.ViewId)
              .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<Todo>()
              .WithMany()
              .HasForeignKey(o => o.TodoId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
