using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class DetalleProductoRepository : IDetalleProductoRepository
    {
        private readonly List<DetalleProductoDTO> DataAlmacenada = new List<DetalleProductoDTO>();

        public DetalleProductoRepository()
        {
            

            var detalle1 = new DetalleProductoDTO()
            {
                Id = Guid.NewGuid(),
                IdMarca = Guid.NewGuid(),
                IdMaterial = Guid.NewGuid(),
                IdProducto=Guid.NewGuid(),
                IdUnidadMedida=Guid.NewGuid()
            };

            DataAlmacenada.Add(detalle1);
        }

        public void Create(DetalleProductoDTO detalleProducto)
        {
            DataAlmacenada.Add(detalleProducto);
        }

        public void Eliminar(Guid Id)
        {
            var detalleAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (detalleAEliminar != null)
            {
                DataAlmacenada.Remove(detalleAEliminar);
            }
            else
            {
                throw new InvalidOperationException("El detalle de producto no existe.");
            }
        }

        public List<DetalleProductoDTO> Get()
        {
            return DataAlmacenada;
        }

        public void ModificarDetalle(Guid Id, DetalleProductoDTO modificarDetalle)
        {
            var detalleAModificar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);
            if (detalleAModificar != null)
            {
                detalleAModificar.IdMarca = modificarDetalle.IdMarca;
                detalleAModificar.IdMaterial = modificarDetalle.IdMaterial;
                detalleAModificar.IdProducto = modificarDetalle.IdProducto;
                detalleAModificar.IdUnidadMedida = modificarDetalle.IdUnidadMedida;
            }
            else
            {
                throw new InvalidOperationException("El detalle de producto no existe.");
            }
        }

        public DetalleProductoDTO GetById(Guid Id)
        {
            var detalleAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (detalleAMostrar != null)
            {
                return detalleAMostrar;
            }
            else
            {
                throw new InvalidOperationException("El detalle de producto no existe.");
            }

        }
    }
}
