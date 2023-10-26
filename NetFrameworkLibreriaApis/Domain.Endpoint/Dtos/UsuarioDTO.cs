using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario.")]
        public string NombreUsuario { get; set; }


        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        public string Contraseña { get; set; }


        [Required(ErrorMessage = "Debe ingresar el id de un empleado.")]
        public Guid IdEmpleado { get; set; }


        [Required(ErrorMessage = "Debe ingresar el id de un rol.")]
        public Guid IdRol { get; set; }
    }
}
