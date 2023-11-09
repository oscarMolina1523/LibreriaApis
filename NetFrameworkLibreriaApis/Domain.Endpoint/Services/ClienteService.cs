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

        public async Task<Cliente> EliminarCliente(Guid Id)
        {
            //_repository.Eliminar(Id);
            Cliente cliente = await GetById(Id);
            await _repository.Eliminar(cliente);
            return cliente;
        }

        public Task<List<Cliente>> GetAll()
        {
            return _repository.Get();
        }

        public async Task<Cliente> ModificarCliente(Guid Id, ClienteDTO cambioCliente)
        {
            //_repository.ModificarCliente(Id, cambioCliente);
            Cliente cliente = await GetById(Id);

            Cliente newCliente = new Cliente
            {
                Id = cliente.Id,
                Nombres=cambioCliente.Nombres,
                Cedula=cambioCliente.Cedula,
                Telefono=cambioCliente.Telefono
            };

            await _repository.ModificarCliente(newCliente);
            return newCliente;
        }

        public async Task<Cliente> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
