using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        Task<List<Material>> Get();

        Material GetById(Guid Id);

        void Create(Material material);

        void Eliminar(Guid Id);
    }
}
