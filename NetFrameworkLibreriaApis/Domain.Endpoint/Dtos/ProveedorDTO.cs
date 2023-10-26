using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class ProveedorDTO
    {
        [Required(ErrorMessage = "Debe ingresar una direccion del proveedor.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripcion del proveedor.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar el numero de telefono del proveedor.")]
        public string Telefono { get; set; }

        public string CorreoElectronico { get; set; }
    }
}
