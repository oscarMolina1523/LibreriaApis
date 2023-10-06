using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class DetalleService : IDetalleService
    {
        private readonly IDetalleProductoRepository _repository;

        public DetalleService(IDetalleProductoRepository repository)
        {
            _repository = repository;
        }

        public DetalleProductoDTO CrearDetalle(DetalleProductoDTO nuevoDetalle)
        {
            DetalleProductoDTO newDetalle = new DetalleProductoDTO()
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

        public List<DetalleProductoDTO> GetAll()
        {
           return _repository.Get();
        }

        public void ModificarDetalle(Guid Id, DetalleProductoDTO cambioDetalle)
        {
            _repository.ModificarDetalle(Id, cambioDetalle);
        }

        public DetalleProductoDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
