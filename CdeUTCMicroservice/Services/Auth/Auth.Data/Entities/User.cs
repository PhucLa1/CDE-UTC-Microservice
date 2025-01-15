using Auth.Data.Entities.Base;
using Auth.Data.Enums;

namespace Auth.Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string WorkPhoneNumber { get; set; } = string.Empty;
        public string MobilePhoneNumber { get; set; } = string.Empty;
        public Guid? LanguageId { get; set; }
        public Guid? CityId { get; set; }
        public DateDisplay DateDisplay { get; set; }
        public TimeDisplay TimeDisplay { get; set; }
        public string Employer { get; set; } = string.Empty;
        public Guid? JobTitleId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime TokenExpired { get; set; }
    }
}
