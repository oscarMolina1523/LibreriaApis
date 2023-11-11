using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IProveedorRepository
    {
        Task<List<Proveedor>> Get();

        Task<Proveedor> GetById(Guid Id);

        void Create(Proveedor proveedor);

        Task Eliminar(Proveedor proveedor);

        Task ModificarProveedor(Proveedor modificarProveedor);
    }
}
