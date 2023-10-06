using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IRolRepository
    {
        List<RolDTO> Get();

        RolDTO GetById(Guid Id);

        void Create(RolDTO rol);

        void Eliminar(Guid Id);
    }
}
