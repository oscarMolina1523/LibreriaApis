using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IDetalleProductoRepository
    {
        Task<List<DetalleProducto>> Get();

        DetalleProducto GetById(Guid Id);

        void Create(DetalleProducto detalleProducto);

        void Eliminar(Guid Id);

        void ModificarDetalle(Guid Id, DetalleProductoDTO modificarDetalle);
    }
}
