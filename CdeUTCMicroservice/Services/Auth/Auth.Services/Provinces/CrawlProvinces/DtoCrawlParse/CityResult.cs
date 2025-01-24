

namespace Auth.Application.Provinces.CrawlProvinces.DtoCrawlParse
{
    public class CityResult
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("codename")]
        public string CodeName { get; set; } = string.Empty;
        [JsonProperty("division_type")]
        public string DivisionType { get; set; } = string.Empty;
        [JsonProperty("phone_code")]
        public int PhoneCode { get; set; }
        [JsonProperty("districts")]
        public List<DistrictResult> Districts { get; set; } = new List<DistrictResult>();
    }
}
