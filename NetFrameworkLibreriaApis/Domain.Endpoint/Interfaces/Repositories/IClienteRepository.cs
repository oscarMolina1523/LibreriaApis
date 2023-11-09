using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> Get();

        Task<Cliente> GetById(Guid Id);

        void Create(Cliente cliente);

        Task Eliminar(Cliente cliente);

        Task ModificarCliente(Cliente modificarCliente);
    }
}
