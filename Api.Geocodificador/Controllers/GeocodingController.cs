using Api.Geocodificador.Models;
using Api.Geocodificador.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Geocodificador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeocodingController : ControllerBase
    {
        private readonly IBackgroundQueue<Process> _backgroundQueue;

        public GeocodingController(IBackgroundQueue<Process> backgroundQueue)
        {
            _backgroundQueue = backgroundQueue;
        }

        [HttpPost]
        public IActionResult ReceivedGeocoding(Process process)
        {
           _backgroundQueue.Enqueue(process);
            return Ok();
        }
    }
}
