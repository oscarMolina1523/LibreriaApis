using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repository;

        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }

        public Rol CrearRol(RolDTO nuevoRol)
        {
            Rol newRol = new Rol()
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

        public Task<List<Rol>> GetAll()
        {
            return _repository.Get();
        }

        public Rol GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
