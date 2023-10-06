namespace Domain.Endpoint.Entities
{
    public class EmpleadoDTO : BaseEntity
    {
        public string Nombres{ get; set; }

        public string Apellidos { get; set; }

        public string Cedula { get; set; }

        public string Telefono { get; set; }

    }
}
