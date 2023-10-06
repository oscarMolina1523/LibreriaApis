using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IProveedorRepository
    {
        List<ProveedorDTO> Get();

        ProveedorDTO GetById(Guid Id);

        void Create(ProveedorDTO proveedor);

        void Eliminar(Guid Id);

        void ModificarProveedor(Guid Id, ProveedorDTO modificarProveedor);
    }
}
