namespace Domain.Endpoint.Entities
{
    public class ClienteDTO:BaseEntity
    {
        public string Nombres { get; set; }

        public string Cedula { get; set; }

        public string Telefono { get; set; }
    }
}
