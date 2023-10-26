using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de cliente.")]
        public string Nombres { get; set; }

        public string Cedula { get; set; }

        public string Telefono { get; set; }
    }
}
