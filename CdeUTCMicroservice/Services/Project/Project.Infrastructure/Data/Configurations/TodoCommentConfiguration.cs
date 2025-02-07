namespace Project.Infrastructure.Data.Configurations
{
    public class TodoCommentConfiguration : IEntityTypeConfiguration<TodoComment>
    {
        public void Configure(EntityTypeBuilder<TodoComment> builder)
        {
            builder.HasOne<Todo>()
                .WithMany()
                .HasForeignKey(o => o.TodoId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
