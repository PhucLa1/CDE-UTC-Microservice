using Auth.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace Auth.Application.Auth.ChangeInfo
{
    public class ChangeInfoRequest : ICommand<ChangeInfoResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? WardId { get; set; }
        public DateDisplay? DateDisplay { get; set; }
        public TimeDisplay? TimeDisplay { get; set; }
        public string? Employer { get; set; }
        public int? JobTitleId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
