

namespace Project.Infrastructure.Data.Configurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Type> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                TypeId => TypeId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => TypeId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
