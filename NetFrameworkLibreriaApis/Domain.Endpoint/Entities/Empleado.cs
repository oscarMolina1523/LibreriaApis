namespace Domain.Endpoint.Entities
{
    public class Empleado  : BaseEntity
    {
        public string Nombres{ get; set; }

        public string Apellidos { get; set; }

        public string Cedula { get; set; }

        public string Telefono { get; set; }

    }
}
