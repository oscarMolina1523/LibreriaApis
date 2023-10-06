using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IDetalleProductoRepository
    {
        List<DetalleProductoDTO> Get();

        DetalleProductoDTO GetById(Guid Id);

        void Create(DetalleProductoDTO detalleProducto);

        void Eliminar(Guid Id);

        void ModificarDetalle(Guid Id, DetalleProductoDTO modificarDetalle);
    }
}
