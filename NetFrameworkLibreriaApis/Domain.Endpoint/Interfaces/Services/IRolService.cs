using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IRolService
    {
        List<RolDTO> GetAll();

        RolDTO GetById(Guid Id);

        RolDTO CrearRol(RolDTO nuevoRol);

        void EliminarRol(Guid Id);
    }
}
