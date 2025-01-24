
namespace Auth.Application.Provinces.GetAllProvinces
{
    public class GetAllProvincesHandler
        (IBaseRepository<City> cityRepository,
        IBaseRepository<District> districtRepository,
        IBaseRepository<Ward> wardRepository)
        : IQueryHandler<GetAllProvincesRequest, ApiResponse<List<GetAllProvincesResponse>>>
    {
        public async Task<ApiResponse<List<GetAllProvincesResponse>>> Handle(GetAllProvincesRequest request, CancellationToken cancellationToken)
        {
            var results = new List<GetAllProvincesResponse>();
            if (request.CityId is not null)
            {
                results = await districtRepository.GetAllQueryAble()
                .Where(e => e.CityId == request.CityId)
                .Select(e => new GetAllProvincesResponse()
                {
                    Name = e.Name,
                    Id = e.Id,
                }).ToListAsync(cancellationToken);
            }
            else if (request.DistrictId is not null)
            {
                results = await wardRepository.GetAllQueryAble()
                .Where(e => e.DistrictId == request.DistrictId)
                .Select(e => new GetAllProvincesResponse()
                {
                    Name = e.Name,
                    Id = e.Id,
                }).ToListAsync(cancellationToken);
            }
            else
            {
                results = await cityRepository.GetAllQueryAble()
                .Select(e => new GetAllProvincesResponse()
                {
                    Name = e.Name,
                    Id = e.Id,
                }).ToListAsync(cancellationToken);
            }
            return new ApiResponse<List<GetAllProvincesResponse>> { Data = results, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
