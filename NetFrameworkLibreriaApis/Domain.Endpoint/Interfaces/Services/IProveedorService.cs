using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IProveedorService
    {
        Task<List<Proveedor>> GetAll();

        Proveedor GetById(Guid Id);

        Proveedor CrearProveedor(ProveedorDTO nuevoProveedor);

        void EliminarProveedor(Guid Id);

        void ModificarProveedor(Guid Id, ProveedorDTO cambioProveedor);
    }
}
