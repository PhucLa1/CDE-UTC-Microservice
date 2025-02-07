using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Abstractions
{
    public class BaseEntity : IAuditable
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("updated_by")]
        public int UpdatedBy { get; set; }
    }
}
