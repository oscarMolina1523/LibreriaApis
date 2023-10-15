using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _repository;

        public ProveedorService(IProveedorRepository repository)
        {
            _repository = repository;
        }

        public Proveedor CrearProveedor(ProveedorDTO nuevoProveedor)
        {
            Proveedor newProveedor = new Proveedor()
            {
                Id = Guid.NewGuid(),
                Direccion = nuevoProveedor.Direccion,
                CorreoElectronico=nuevoProveedor.CorreoElectronico,
                Telefono = nuevoProveedor.Telefono, 
                Descripcion=nuevoProveedor.Descripcion
            };

            _repository.Create(newProveedor);
            return newProveedor;
        }

        public void EliminarProveedor(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public Task<List<Proveedor>> GetAll()
        {
            return _repository.Get();
        }

        public void ModificarProveedor(Guid Id, ProveedorDTO cambioProveedor)
        {
            _repository.ModificarProveedor(Id, cambioProveedor);
        }

        public Proveedor GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
