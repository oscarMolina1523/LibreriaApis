using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class ProductoDTO
    {
        [Required(ErrorMessage = "Debe ingresar una descripcion para el producto.")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "Debe ingresar un id de la categoria.")]
        public Guid IdCategoria { get; set; }
    }
}
