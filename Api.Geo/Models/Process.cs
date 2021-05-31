using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Geo.Models
{
    public class Process
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string State { get; set; }
        public string LocationId { get; set; }

        public virtual Location Location { get; set; }

        public Process() { }
    }
}
