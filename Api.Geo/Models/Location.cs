using Api.Geo.Models.DTO;

namespace Api.Geo.Models
{
    public class Location
    {
        public string Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public Location() { }
        public Location(Localizacion localizacion) 
        {
            Street = $"{localizacion.Numero} {localizacion.Calle}";
            City = localizacion.Ciudad;
            State = localizacion.Provincia;
            Country = localizacion.Pais;
            PostalCode = localizacion.Codigo_postal;
        }
    }
}
