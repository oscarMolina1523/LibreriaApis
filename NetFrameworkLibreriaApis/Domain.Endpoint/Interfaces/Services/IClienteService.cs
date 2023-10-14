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

        Cliente GetById(Guid Id);

        Cliente CrearCliente(ClienteDTO nuevoCliente);

        void EliminarCliente(Guid Id);

        void ModificarCliente(Guid Id, ClienteDTO cambioCliente);
    }
}
