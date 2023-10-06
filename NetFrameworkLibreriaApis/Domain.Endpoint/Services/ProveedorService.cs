using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _repository;

        public ProveedorService(IProveedorRepository repository)
        {
            _repository = repository;
        }

        public ProveedorDTO CrearProveedor(ProveedorDTO nuevoProveedor)
        {
            ProveedorDTO newProveedor = new ProveedorDTO()
            {
                Id = Guid.NewGuid(),
                NombreProveedor = nuevoProveedor.NombreProveedor,
                Direccion = nuevoProveedor.Direccion,
                CorreoEletronico=nuevoProveedor.CorreoEletronico,
                Telefono = nuevoProveedor.Telefono
            };

            _repository.Create(newProveedor);
            return newProveedor;
        }

        public void EliminarProveedor(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public List<ProveedorDTO> GetAll()
        {
            return _repository.Get();
        }

        public void ModificarProveedor(Guid Id, ProveedorDTO cambioProveedor)
        {
            _repository.ModificarProveedor(Id, cambioProveedor);
        }

        public ProveedorDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
