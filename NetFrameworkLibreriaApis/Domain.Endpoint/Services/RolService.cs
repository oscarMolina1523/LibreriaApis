using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repository;

        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }

        public RolDTO CrearRol(RolDTO nuevoRol)
        {
            RolDTO newRol = new RolDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionRol = nuevoRol.DescripcionRol,
             
            };

            _repository.Create(newRol);
            return newRol;
        }

        public void EliminarRol(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public List<RolDTO> GetAll()
        {
            return _repository.Get();
        }

        public RolDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
