using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class ClienteRepository : IClienteRepository
    {
        private readonly List<ClienteDTO> DataAlmacenada = new List<ClienteDTO>();

        public ClienteRepository()
        {
            

            var cliente1 = new ClienteDTO()
            {
                Id = Guid.NewGuid(),
                Nombres = "Eduardo Nahum Pichardo",
                Cedula = "1548-154895-555U",
                Telefono = "1548-9584"
            };

            DataAlmacenada.Add(cliente1);
        }

        public void Create(ClienteDTO cliente)
        {
            DataAlmacenada.Add(cliente);
        }

        public void Eliminar(Guid Id)
        {
            var clienteAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (clienteAEliminar != null)
            {
                DataAlmacenada.Remove(clienteAEliminar);
            }
            else
            {
                throw new InvalidOperationException("El cliente no existe.");
            }
        }

        public List<ClienteDTO> Get()
        {
            return DataAlmacenada;
        }

        public void ModificarCliente(Guid Id, ClienteDTO modificarCliente)
        {
            var clienteAModificar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);
            if (clienteAModificar != null)
            {
                clienteAModificar.Nombres = modificarCliente.Nombres;
                clienteAModificar.Cedula = modificarCliente.Cedula;
                clienteAModificar.Telefono = modificarCliente.Telefono;
            }
            else
            {
                throw new InvalidOperationException("El cliente no existe.");
            }
        }

        public ClienteDTO GetById(Guid Id)
        {
            var clienteAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (clienteAMostrar != null)
            {
                return clienteAMostrar;
            }
            else
            {
                throw new InvalidOperationException("El cliente no existe.");
            }

        }
    }
}
