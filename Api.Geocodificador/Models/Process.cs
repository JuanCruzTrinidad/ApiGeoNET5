
namespace Api.Geocodificador.Models
{
    public class Process
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string State { get; set; }
        public Location Location { get; set; }

        public Process() { }
    }
}
