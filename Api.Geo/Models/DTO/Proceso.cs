namespace Api.Geo.Models.DTO
{
    public class Proceso
    {
        public string Id { get; set; }
        public double Latitud {get; set;}
        public double Longitud { get; set; }
        public string Estado { get; set; }

        public Proceso() { }
        public Proceso(Process process)
        {
            Id = process.Id;
            Latitud = process.Latitude;
            Longitud = process.Longitude;
            Estado = process.State;
        }

        public Process ToEntity()
        {
            return new Process
            {
                Id = Id,
                Latitude = Latitud,
                Longitude = Longitud,
                State = Estado
            };
        }

    }
}
