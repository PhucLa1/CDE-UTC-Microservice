namespace Project.Infrastructure.Data.Configurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Type> builder)
        {
            builder.HasOne(x => x.Project)
                .WithMany(x => x.Types)
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
