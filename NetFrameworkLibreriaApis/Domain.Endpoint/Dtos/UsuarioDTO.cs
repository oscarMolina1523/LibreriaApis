using System;

namespace Domain.Endpoint.Dtos
{
    public class UsuarioDTO
    {
        public string NombreUsuario { get; set; }

        public string Contraseña { get; set; }

        public Guid IdEmpleado { get; set; }

        public Guid IdRol { get; set; }
    }
}
