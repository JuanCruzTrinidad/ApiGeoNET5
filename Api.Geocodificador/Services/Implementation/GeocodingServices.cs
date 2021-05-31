using Api.Geocodificador.Models;
using Api.Geocodificador.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Geocodificador.Services.Implementation
{
    public class GeocodingServices : IGeocodingServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GeocodingServices> _logger;
        public GeocodingServices(IConfiguration configuration, ILogger<GeocodingServices> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task Geocoding(Process process)
        {
            HttpClient client = new HttpClient();
            Location location = process.Location;
            client.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
            HttpResponseMessage response = await  client.GetAsync($"/search?format=geojson&street={location.Street}&city={location.City}&country={location.Country}&postalcode={location.PostalCode}&state={location.State}&email=juancruztrinidad97@gmail.com");
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                JsonDocument result = JsonSerializer.Deserialize<JsonDocument>(body);
                string geometryJson = result.RootElement.GetProperty("features")[0].GetProperty("geometry").GetRawText();
                Geometry geometry = JsonSerializer.Deserialize<Geometry>(geometryJson, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
                process.Latitude = geometry.Coordinates[1];
                process.Longitude = geometry.Coordinates[0];
                bool send = await SendGeocodingResult(process);
                if(send)
                {
                    _logger.LogInformation("Succesfully return process complete.");
                }
                else
                {
                    _logger.LogError("An error has occurred in the request");
                }
            }
        }

        private async Task<bool> SendGeocodingResult(Process process)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["UrlApiGeo"]);
            HttpResponseMessage response = await client.PostAsJsonAsync("/resultado", process);
            return response.IsSuccessStatusCode;
        }
    }
}
