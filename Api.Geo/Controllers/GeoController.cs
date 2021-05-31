using Api.Geo.Models;
using Api.Geo.Models.DTO;
using Api.Geo.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Geo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly IGeoLocateServices _geocodingServices;
        public GeoController( IGeoLocateServices geocodingServices) 
        {
            _geocodingServices = geocodingServices;
        }

        [Route("/geolocalizar")]
        [HttpPost]
        public async Task<IActionResult> GeolocateAsync(Localizacion localizacion)
        {
            Location location = localizacion.ToEntity();
            string idProcess = await _geocodingServices.SendGeocoding(location);
            return Accepted(new {  Id = idProcess });
        }

        [Route("/resultado")]
        [HttpPost]
        public async Task<IActionResult> GeolocateResultAsync(Process process)
        {
            await _geocodingServices.ReceivedGeocodingResult(process);
            return Ok();
        }

        [HttpGet("/geocodificar")]
        public async Task<IActionResult> GetGeolocateAsync(string id)
        {
            Process process = await _geocodingServices.GetGeolocate(id);
            return Ok(new Proceso(process));
        }
    }
}
