using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public Cliente CrearCliente(ClienteDTO nuevoCliente)
        {
            Cliente newCliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nombres = nuevoCliente.Nombres,
                Cedula = nuevoCliente.Cedula,
                Telefono = nuevoCliente.Telefono
            };

            _repository.Create(newCliente);
            return newCliente;
        }

        public void EliminarCliente(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public Task<List<Cliente>> GetAll()
        {
            return _repository.Get();
        }

        public void ModificarCliente(Guid Id, ClienteDTO cambioCliente)
        {
            _repository.ModificarCliente(Id, cambioCliente);
        }

        public Cliente GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
