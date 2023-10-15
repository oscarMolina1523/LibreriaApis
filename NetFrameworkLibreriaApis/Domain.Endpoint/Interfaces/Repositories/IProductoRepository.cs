using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IProductoRepository
    {
        Task<List<Producto>> Get();

        Producto GetById(Guid Id);

        void Create(Producto producto);

        void Eliminar(Guid Id);

        void ModificarProducto(Guid Id, ProductoDTO modificarProducto);
    }
}
