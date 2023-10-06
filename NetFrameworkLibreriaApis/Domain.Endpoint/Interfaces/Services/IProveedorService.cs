using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IProveedorService
    {
        List<ProveedorDTO> GetAll();

        ProveedorDTO GetById(Guid Id);

        ProveedorDTO CrearProveedor(ProveedorDTO nuevoProveedor);

        void EliminarProveedor(Guid Id);

        void ModificarProveedor(Guid Id, ProveedorDTO cambioProveedor);
    }
}
