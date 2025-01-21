using Auth.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace Auth.Application.Auth.ChangeInfo
{
    public class ChangeInfoRequest : ICommand<ChangeInfoResponse>
    {
        public string? Email { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public Guid? LanguageId { get; set; }
        public Guid? CityId { get; set; }
        public DateDisplay? DateDisplay { get; set; }
        public TimeDisplay? TimeDisplay { get; set; }
        public string? Employer { get; set; }
        public Guid? JobTitleId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
