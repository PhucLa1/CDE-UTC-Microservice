
namespace Project.Infrastructure.Data.Configurations
{
    public class AnnotationConfiguration : IEntityTypeConfiguration<Annotation>
    {
        public void Configure(EntityTypeBuilder<Annotation> builder)
        {
            builder.HasOne(x => x.View)
                .WithMany(x => x.Annotations)
                .HasForeignKey(o => o.ViewId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
