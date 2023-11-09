using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class DetalleService : IDetalleService
    {
        private readonly IDetalleProductoRepository _repository;

        public DetalleService(IDetalleProductoRepository repository)
        {
            _repository = repository;
        }

        public DetalleProducto CrearDetalle(DetalleProductoDTO nuevoDetalle)
        {
            DetalleProducto newDetalle = new DetalleProducto()
            {
                Id = Guid.NewGuid(),
                IdMarca=nuevoDetalle.IdMarca,
                IdMaterial=nuevoDetalle.IdMaterial,
                IdProducto=nuevoDetalle.IdProducto,
                IdUnidadMedida=nuevoDetalle.IdUnidadMedida
            };

            _repository.Create(newDetalle);
            return newDetalle;
        }

        public async Task<DetalleProducto> EliminarDetalle(Guid Id)
        {
            //_repository.Eliminar(Id);
            DetalleProducto detalle = await GetById(Id);
            await _repository.Eliminar(detalle);
            return detalle;
        }

        public Task<List<DetalleProducto>> GetAll()
        {
           return _repository.Get();
        }

        public async Task<DetalleProducto> ModificarDetalle(Guid Id, DetalleProductoDTO cambioDetalle)
        {
            //_repository.ModificarDetalle(Id, cambioDetalle);
            DetalleProducto detalle = await GetById(Id);

            DetalleProducto newDetalle = new DetalleProducto
            {
                Id = detalle.Id,
                IdMarca = cambioDetalle.IdMarca,
                IdMaterial = cambioDetalle.IdMaterial,
                IdProducto = cambioDetalle.IdProducto,
                IdUnidadMedida= cambioDetalle.IdUnidadMedida
            };

            await _repository.ModificarDetalle(newDetalle);
            return newDetalle;
        }

        public async Task<DetalleProducto> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
