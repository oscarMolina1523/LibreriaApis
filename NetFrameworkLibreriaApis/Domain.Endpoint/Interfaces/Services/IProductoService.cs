using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IProductoService
    {
        List<ProductoDTO> GetAll();

        ProductoDTO GetById(Guid Id);

        ProductoDTO CrearProducto(ProductoDTO nuevoProducto);

        void EliminarProducto(Guid Id);

        void ModificarProducto(Guid Id, ProductoDTO cambioProducto);
    }
}
