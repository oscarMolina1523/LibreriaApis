using System;

namespace Domain.Endpoint.Entities
{
    public class Producto:BaseEntity
    {
        public string DescripcionProducto { get; set; }

        public Guid IdCategoria { get; set; }
    }
}
