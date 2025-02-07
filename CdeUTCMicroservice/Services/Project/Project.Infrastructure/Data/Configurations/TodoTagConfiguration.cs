namespace Project.Infrastructure.Data.Configurations
{
    public class TodoTagConfiguration : IEntityTypeConfiguration<TodoTag>
    {
        public void Configure(EntityTypeBuilder<TodoTag> builder)
        {
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
