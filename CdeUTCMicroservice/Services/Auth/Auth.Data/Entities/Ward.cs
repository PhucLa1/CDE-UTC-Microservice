using Auth.Data.Entities.Base;

namespace Auth.Data.Entities
{
    public class Ward : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string CodeName { get; set; } = string.Empty;
        public string ShortCodeName { get; set; } = string.Empty;
        public Guid DistrictId { get; set; }
        public string DivisionType { get; set; } = string.Empty;
    }
}
