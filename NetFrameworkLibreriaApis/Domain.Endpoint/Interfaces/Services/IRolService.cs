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

        Task<Rol> GetById(Guid Id);

        Rol CrearRol(RolDTO nuevoRol);

        Task<Rol> EliminarRol(Guid Id);
    }
}
