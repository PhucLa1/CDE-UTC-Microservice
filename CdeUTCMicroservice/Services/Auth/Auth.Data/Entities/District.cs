using Auth.Data.Entities.Base;

namespace Auth.Data.Entities
{
    public class District : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string CodeName { get; set; } = string.Empty;
        public string ShortCodeName { get; set; } = string.Empty;
        public string DivisionType { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public ICollection<Ward> Wards { get; set; } = new List<Ward>();
    }
}
