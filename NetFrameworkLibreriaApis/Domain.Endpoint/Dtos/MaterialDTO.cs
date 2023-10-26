using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class MaterialDTO
    {
        [Required(ErrorMessage = "Debe ingresar una descripcion para el material.")]
        public string DescripcionMaterial { get; set; }
    }
}
