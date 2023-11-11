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

        public async Task<Proveedor> EliminarProveedor(Guid Id)
        {
            //_repository.Eliminar(Id);
            Proveedor proveedor = await GetById(Id);
            await _repository.Eliminar(proveedor);
            return proveedor;
        }

        public Task<List<Proveedor>> GetAll()
        {
            return _repository.Get();
        }

        public async Task<Proveedor> ModificarProveedor(Guid Id, ProveedorDTO cambioProveedor)
        {
            //_repository.ModificarProveedor(Id, cambioProveedor);
            Proveedor proveedor = await GetById(Id);

            Proveedor newProveedor = new Proveedor
            {
                Id = proveedor.Id,
                Direccion=cambioProveedor.Direccion,
                Descripcion=cambioProveedor.Descripcion,
                Telefono=cambioProveedor.Telefono,
                CorreoElectronico=cambioProveedor.CorreoElectronico
            };

            await _repository.ModificarProveedor(newProveedor);
            return newProveedor;
        }

        public async Task<Proveedor> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
