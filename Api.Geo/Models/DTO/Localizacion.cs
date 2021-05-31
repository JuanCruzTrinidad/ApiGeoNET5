namespace Api.Geo.Models.DTO
{
    public class Localizacion
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Codigo_postal { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }

        public Localizacion() { }

        public Location ToEntity()
        {
            return new Location
            {
                Street = $"{this.Numero} {this.Calle}",
                City = this.Ciudad,
                State = this.Provincia,
                Country = this.Pais,
                PostalCode = this.Codigo_postal
            };
        }
    }
}
