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
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? WardId { get; set; }
        public DateDisplay DateDisplay { get; set; } = DateDisplay.Iso8601;
        public TimeDisplay TimeDisplay { get; set; } = TimeDisplay.TwelveHour;
        public string Employer { get; set; } = string.Empty;
        public Guid? JobTitleId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime TokenExpired { get; set; }
        public City? City { get; set; }
        public Ward? Ward { get; set; }
        public District? District { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime TimeExpired { get; set; }
        public bool CanChangePassword { get; set; } = false;
    }
}
