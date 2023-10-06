using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public ClienteDTO CrearCliente(ClienteDTO nuevoCliente)
        {
            ClienteDTO newCliente = new ClienteDTO()
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

        public List<ClienteDTO> GetAll()
        {
            return _repository.Get();
        }

        public void ModificarCliente(Guid Id, ClienteDTO cambioCliente)
        {
            _repository.ModificarCliente(Id, cambioCliente);
        }

        public ClienteDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
