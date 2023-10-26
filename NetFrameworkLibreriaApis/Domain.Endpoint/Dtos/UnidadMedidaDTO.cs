using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class UnidadMedidaDTO
    {
        [Required(ErrorMessage = "Debe ingresar una descripcion para la unidad de medida.")]
        public string DescripcionMedida { get; set; }
    }
}
