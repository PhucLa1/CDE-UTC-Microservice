
using Auth.Application.Provinces.CrawlProvinces.DtoCrawlParse;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Auth.Application.Provinces.CrawlProvinces
{
    public class CrawlProvincesHandler
        (IBaseRepository<City> cityRepository,
        HttpClient httpClient)
        : ICommandHandler<CrawlProvincesRequest, CrawlProvincesResponse>
    {
        public async Task<CrawlProvincesResponse> Handle(CrawlProvincesRequest request, CancellationToken cancellationToken)
        {
            //Gọi đến API Provinces
            string endpoint = "https://provinces.open-api.vn/api/?depth=3";
            var response = await httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode is true)
            {
                var json = await response.Content.ReadAsStringAsync();
                var provinceResults = JsonSerializer.Deserialize<List<CityResult>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;

                //Xóa hết DB đi đã
                var city = await cityRepository.GetAllQueryAble().ToListAsync();
                if(city is not null || city!.Count is not 0)
                {
                    cityRepository.GetDbContext.RemoveRange(city);
                    await cityRepository.SaveChangeAsync(cancellationToken);

                }         
                //Lưu vào DB
                var citiesModels = ConvertToModel(provinceResults);
                await cityRepository.AddRangeAsync(citiesModels, cancellationToken);
                await cityRepository.SaveChangeAsync(cancellationToken);

            }
            return new CrawlProvincesResponse() { Data = true, Message = Message.CRAWL_SUCCESSFULLY };
        }

        private List<City> ConvertToModel(List<CityResult> cityResult)
        {
            var cities = new List<City>();
            foreach (var cityResultItem in cityResult)
            {
                var city = new City()
                {
                    Name = cityResultItem.Name,
                    CodeName = cityResultItem.CodeName,
                    DivisionType = cityResultItem.DivisionType,
                    PhoneCode = cityResultItem.PhoneCode
                };
                //Duyệt qua list districts
                foreach (var districtResultItem in cityResultItem.Districts)
                {
                    var district = new District()
                    {
                        Name = districtResultItem.Name,
                        CodeName = districtResultItem.CodeName,
                        ShortCodeName = districtResultItem.ShortCodename,
                        DivisionType = districtResultItem.DivisionType,
                        CityId = city.Id
                    };
                    city.Districts.Add(district);
                    foreach (var wardResult in districtResultItem.Wards)
                    {
                        var ward = new Ward()
                        {
                            Name = wardResult.Name,
                            CodeName = wardResult.CodeName,
                            ShortCodeName = wardResult.ShortCodename,
                            DivisionType = wardResult.DivisionType,
                            DistrictId = district.Id
                        };
                        district.Wards.Add(ward);
                    }
                }
                cities.Add(city);
            }

            return cities;
        }
    }
}
