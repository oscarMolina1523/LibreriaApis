using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IDetalleService
    {
        Task<List<DetalleProducto>> GetAll();

        Task<DetalleProducto> GetById(Guid Id);

        DetalleProducto CrearDetalle(DetalleProductoDTO nuevoDetalle);

        Task<DetalleProducto> EliminarDetalle(Guid Id);

        Task<DetalleProducto> ModificarDetalle(Guid Id, DetalleProductoDTO cambioDetalle);
    }
}
