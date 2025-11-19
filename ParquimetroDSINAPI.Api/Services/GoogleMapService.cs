using Microsoft.Extensions.Configuration;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Services
{
    public class GoogleMapService : IMapService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleMapService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["GoogleMaps:ApiKey"]!;
        }

        public async Task<string> GetCoordinatesAsync(string address)
        {
            var url = $"geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);

            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
