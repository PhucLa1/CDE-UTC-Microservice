using Auth.Data.Entities.Base;

namespace Auth.Data.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string UUID { get; set; } = string.Empty;
    }
}
