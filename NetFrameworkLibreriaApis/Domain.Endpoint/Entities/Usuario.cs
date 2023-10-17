using System;

namespace Domain.Endpoint.Entities
{
    public class Usuario:BaseEntity
    {
        public string NombreUsuario { get; set; }

        public string Contraseña { get; set; }

        public Guid IdEmpleado { get; set; }

        public Guid IdRol { get; set; }
    }
}
