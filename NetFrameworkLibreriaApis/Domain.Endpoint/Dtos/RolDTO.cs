using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class RolDTO
    {
        [Required(ErrorMessage = "Debe ingresar una descripcion para el rol.")]
        public string DescripcionRol { get; set; }
    }
}
