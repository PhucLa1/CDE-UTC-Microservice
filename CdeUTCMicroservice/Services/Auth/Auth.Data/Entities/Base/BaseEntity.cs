using Auth.Data.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Data.Entities.Base
{
    public class BaseEntity : IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
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
