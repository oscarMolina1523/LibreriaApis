using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        List<ClienteDTO> Get();

        ClienteDTO GetById(Guid Id);

        void Create(ClienteDTO cliente);

        void Eliminar(Guid Id);

        void ModificarCliente(Guid Id, ClienteDTO modificarCliente);
    }
}
