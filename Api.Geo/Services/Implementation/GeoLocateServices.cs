using Api.Geo.Models;
using Api.Geo.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Geo.Services.Implementation
{
    public class GeoLocateServices : IGeoLocateServices
    {
        private readonly GeoContext _context;
        private readonly IConfiguration _configuration;

        public GeoLocateServices(GeoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Process> GetGeolocate(string id) => await _context.Processes.FirstOrDefaultAsync(p => p.Id == id);

        public async Task ReceivedGeocodingResult(Process process)
        {
            process.State = "TERMINADO";
            _context.Processes.Update(process);
            await _context.SaveChangesAsync();
        }

        public async Task<string> SendGeocoding(Location location)
        {
            location.Id = Guid.NewGuid().ToString();
            Process process = new Process();
            process.Id = Guid.NewGuid().ToString();
            process.State = "PROCESANDO";
            process.Location = location;
            _context.Processes.Add(process);
            await _context.SaveChangesAsync();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetValue<string>("GeocodingURL"));
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/Geocoding", process, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            string algo = await response.Content.ReadAsStringAsync();
            return  response.IsSuccessStatusCode ? process.Id : "Failed Request";
        }
    }
}
