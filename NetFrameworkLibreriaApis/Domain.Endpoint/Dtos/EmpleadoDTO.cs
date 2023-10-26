using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class EmpleadoDTO 
    {
        [Required(ErrorMessage = "Debe ingresar un nombre del empleado.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido del empleado.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cedula del empleado.")]
        public string Cedula { get; set; }

        public string Telefono { get; set; }
    }
}
