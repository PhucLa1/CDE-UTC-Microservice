using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("created_by")]
        public Guid CreatedBy { get; set; }
        [Column("updated_by")]
        public Guid UpdatedBy { get; set; }
    }
}
