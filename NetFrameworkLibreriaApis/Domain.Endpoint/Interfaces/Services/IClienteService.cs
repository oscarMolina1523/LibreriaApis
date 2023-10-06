using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IClienteService
    {
        List<ClienteDTO> GetAll();

        ClienteDTO GetById(Guid Id);

        ClienteDTO CrearCliente(ClienteDTO nuevoCliente);

        void EliminarCliente(Guid Id);

        void ModificarCliente(Guid Id, ClienteDTO cambioCliente);
    }
}
