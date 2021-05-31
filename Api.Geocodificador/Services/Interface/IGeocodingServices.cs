using Api.Geocodificador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Geocodificador.Services.Interface
{
    public interface IGeocodingServices
    {
        Task Geocoding(Process process);
    }
}
