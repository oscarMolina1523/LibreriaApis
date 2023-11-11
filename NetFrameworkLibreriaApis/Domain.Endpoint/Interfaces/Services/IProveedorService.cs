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

        Task<Proveedor> GetById(Guid Id);

        Proveedor CrearProveedor(ProveedorDTO nuevoProveedor);

        Task<Proveedor> EliminarProveedor(Guid Id);

        Task<Proveedor> ModificarProveedor(Guid Id, ProveedorDTO cambioProveedor);
    }
}
