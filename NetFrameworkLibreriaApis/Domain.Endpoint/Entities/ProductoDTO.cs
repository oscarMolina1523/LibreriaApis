using System;

namespace Domain.Endpoint.Entities
{
    public class ProductoDTO:BaseEntity
    {
        public string DescripcionProducto { get; set; }

        public Guid IdCategoria { get; set; }
    }
}
