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

        Cliente GetById(Guid Id);

        void Create(Cliente cliente);

        void Eliminar(Guid Id);

        void ModificarCliente(Guid Id, ClienteDTO modificarCliente);
    }
}
