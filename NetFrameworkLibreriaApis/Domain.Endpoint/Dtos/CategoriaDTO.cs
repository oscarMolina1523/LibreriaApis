using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class CategoriaDTO
    {
        [Required(ErrorMessage = "Debe ingresar una descripcion para la categoria.")]
        public string Descripcion { get; set; }
    }
}
