using Auth.Data.Enums;

namespace Auth.Application.Auth.GetInfo
{
    public class GetInfoResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? WardId { get; set; }
        public DateDisplay? DateDisplay { get; set; }
        public TimeDisplay? TimeDisplay { get; set; }
        public string? Employer { get; set; }
        public int? JobTitleId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
