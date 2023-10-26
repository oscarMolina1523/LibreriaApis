using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class MarcaDTO
    {
        [Required(ErrorMessage = "Debe ingresar una descripcion para la marca.")]
        public string DescripcionMarca { get; set; }
    }
}
