namespace Domain.Endpoint.Entities
{
    public class ProveedorDTO:BaseEntity
    {
        public string NombreProveedor { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string CorreoEletronico { get; set; }
    }
}
