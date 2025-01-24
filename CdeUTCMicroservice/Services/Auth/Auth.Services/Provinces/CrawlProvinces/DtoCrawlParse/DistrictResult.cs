namespace Auth.Application.Provinces.CrawlProvinces.DtoCrawlParse
{
    public class DistrictResult
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("codename")]
        public string CodeName { get; set; } = string.Empty;
        [JsonProperty("division_type")]
        public string DivisionType { get; set; } = string.Empty;
        [JsonProperty("short_codename")]
        public string ShortCodename { get; set; } = string.Empty;
        [JsonProperty("wards")] 
        public List<WardResult> Wards { get; set; } = new List<WardResult>();
    }
}
