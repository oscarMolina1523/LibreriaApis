using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        List<MaterialDTO> Get();

        MaterialDTO GetById(Guid Id);

        void Create(MaterialDTO material);

        void Eliminar(Guid Id);
    }
}
