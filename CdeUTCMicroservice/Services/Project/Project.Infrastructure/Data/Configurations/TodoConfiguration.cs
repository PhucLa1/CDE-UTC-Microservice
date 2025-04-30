namespace Project.Infrastructure.Data.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {            
            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);

            
        }
    }
}
