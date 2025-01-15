
namespace Project.Infrastructure.Data.Configurations
{
    public class TodoCommentConfiguration : IEntityTypeConfiguration<TodoComment>
    {
        public void Configure(EntityTypeBuilder<TodoComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                TodoCommentId => TodoCommentId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => TodoCommentId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<Todo>()
                .WithMany()
                .HasForeignKey(o => o.TodoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
