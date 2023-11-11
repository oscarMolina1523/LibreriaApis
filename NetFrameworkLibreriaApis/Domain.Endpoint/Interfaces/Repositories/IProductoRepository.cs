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

        Task<Producto> GetById(Guid Id);

        void Create(Producto producto);

        Task Eliminar(Producto producto);

        Task ModificarProducto(Producto modificarProducto);
    }
}
