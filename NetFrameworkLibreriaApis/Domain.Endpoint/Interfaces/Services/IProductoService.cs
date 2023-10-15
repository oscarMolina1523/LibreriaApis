using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> GetAll();

        Producto GetById(Guid Id);

        Producto CrearProducto(ProductoDTO nuevoProducto);

        void EliminarProducto(Guid Id);

        void ModificarProducto(Guid Id, ProductoDTO cambioProducto);
    }
}
