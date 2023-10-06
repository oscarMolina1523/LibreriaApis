using System;

namespace Domain.Endpoint.Entities
{
    public class DetalleProductoDTO:BaseEntity
    {
        public Guid IdUnidadMedida { get; set; }

        public Guid IdMarca { get; set; }

        public Guid IdMaterial { get; set; }

        public Guid IdProducto { get; set; }
    }
}
