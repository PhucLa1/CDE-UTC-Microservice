namespace Auth.Application.Provinces.GetAllProvinces
{
    public class GetAllProvincesRequest : IQuery<ApiResponse<List<GetAllProvincesResponse>>>
    {
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
    }
}
