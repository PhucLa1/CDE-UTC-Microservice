using Auth.Data.Entities.Base;
using ServiceStack.DataAnnotations;

namespace Auth.Data.Entities
{
    public class Language : BaseEntity
    {
        [Unique]
        public string Name { get; set; } = string.Empty;
        public string UUID { get; set; } = string.Empty;
    }
}
