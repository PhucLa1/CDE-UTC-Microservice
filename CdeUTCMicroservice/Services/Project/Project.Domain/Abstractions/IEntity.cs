using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Abstractions
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }

    public interface IEntity
    {
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
