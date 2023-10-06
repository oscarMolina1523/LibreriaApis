using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IDetalleService
    {
        List<DetalleProductoDTO> GetAll();

        DetalleProductoDTO GetById(Guid Id);

        DetalleProductoDTO CrearDetalle(DetalleProductoDTO nuevoDetalle);

        void EliminarDetalle(Guid Id);

        void ModificarDetalle(Guid Id, DetalleProductoDTO cambioDetalle);
    }
}
