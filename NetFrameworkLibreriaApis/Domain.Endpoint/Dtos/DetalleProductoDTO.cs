using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.Dtos
{
    public class DetalleProductoDTO
    {
        [Required(ErrorMessage = "Debe ingresar un id de la unidad de medida.")]
        public Guid IdUnidadMedida { get; set; }

        [Required(ErrorMessage = "Debe ingresar un id de la marca.")]
        public Guid IdMarca { get; set; }

        [Required(ErrorMessage = "Debe ingresar un id del material.")]
        public Guid IdMaterial { get; set; }

        [Required(ErrorMessage = "Debe ingresar un id del producto.")]
        public Guid IdProducto { get; set; }
    }
}
