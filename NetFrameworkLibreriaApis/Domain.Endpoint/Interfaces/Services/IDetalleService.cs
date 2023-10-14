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

        DetalleProducto GetById(Guid Id);

        DetalleProducto CrearDetalle(DetalleProductoDTO nuevoDetalle);

        void EliminarDetalle(Guid Id);

        void ModificarDetalle(Guid Id, DetalleProductoDTO cambioDetalle);
    }
}
