using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IProductoRepository
    {
        List<ProductoDTO> Get();

        ProductoDTO GetById(Guid Id);

        void Create(ProductoDTO producto);

        void Eliminar(Guid Id);

        void ModificarProducto(Guid Id, ProductoDTO modificarProducto);
    }
}
