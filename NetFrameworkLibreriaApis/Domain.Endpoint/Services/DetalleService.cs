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

        public void EliminarDetalle(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public Task<List<DetalleProducto>> GetAll()
        {
           return _repository.Get();
        }

        public void ModificarDetalle(Guid Id, DetalleProductoDTO cambioDetalle)
        {
            _repository.ModificarDetalle(Id, cambioDetalle);
        }

        public DetalleProducto GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
