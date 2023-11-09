using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAll();

        Task<Cliente> GetById(Guid Id);

        Cliente CrearCliente(ClienteDTO nuevoCliente);

        Task<Cliente> EliminarCliente(Guid Id);

        Task<Cliente> ModificarCliente(Guid Id, ClienteDTO cambioCliente);
    }
}
