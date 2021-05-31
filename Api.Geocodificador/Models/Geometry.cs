using System.Collections.Generic;

namespace Api.Geocodificador.Models
{
    public class Geometry
    {
        public string Type { get; set; }
        public List<double> Coordinates { get; set; } = new List<double>();

        public Geometry() { }
    }
}
