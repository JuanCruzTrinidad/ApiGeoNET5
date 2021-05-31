using Api.Geo.Models;
using System.Threading.Tasks;

namespace Api.Geo.Services.Interface
{
    public interface IGeoLocateServices
    {
        Task<string> SendGeocoding(Location location); //Guardo la peticion en la DB y la mando a la otra API para que haga la peticion.
        Task ReceivedGeocodingResult(Process process); //Recibo el resultado una vez que la otra API termina. Actualizo en la db el proceso.
        Task<Process> GetGeolocate(string id); //Recibo el id y consulto el estado del proceso.
    }
}
