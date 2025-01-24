using Auth.Data.Enums;

namespace Auth.Application.Auth.GetInfo
{
    public class GetInfoResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? WardId { get; set; }
        public DateDisplay? DateDisplay { get; set; }
        public TimeDisplay? TimeDisplay { get; set; }
        public string? Employer { get; set; }
        public Guid? JobTitleId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
