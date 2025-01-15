using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event.Core.Entities.Base
{
    public class BaseEntity : IAuditable
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
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
