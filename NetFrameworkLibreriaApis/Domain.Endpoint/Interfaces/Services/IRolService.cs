using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IRolService
    {
        Task<List<Rol>> GetAll();

        Rol GetById(Guid Id);

        Rol CrearRol(RolDTO nuevoRol);

        void EliminarRol(Guid Id);
    }
}
