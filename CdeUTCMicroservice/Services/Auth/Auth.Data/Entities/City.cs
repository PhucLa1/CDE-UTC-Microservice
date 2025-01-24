using Auth.Data.Entities.Base;

namespace Auth.Data.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string CodeName { get; set; } = string.Empty;
        public string DivisionType { get; set; } = string.Empty;
        public int PhoneCode { get; set; }
        public ICollection<District> Districts { get; set; } = new List<District>();
        public ICollection<Ward> Wards { get; set; } = new List<Ward>();
    }
}
