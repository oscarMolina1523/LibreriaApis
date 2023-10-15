using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IRolRepository
    {
        Task<List<Rol>> Get();

        Rol GetById(Guid Id);

        void Create(Rol rol);

        void Eliminar(Guid Id);
    }
}
