namespace Auth.Application.Provinces.GetAllProvinces
{
    public class GetAllProvincesRequest : IQuery<ApiResponse<List<GetAllProvincesResponse>>>
    {
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
    }
}
