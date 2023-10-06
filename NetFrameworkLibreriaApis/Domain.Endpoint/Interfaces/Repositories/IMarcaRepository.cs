using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IMarcaRepository
    {
        List<MarcaDTO> Get();

        MarcaDTO GetById(Guid Id);

        void Create(MarcaDTO marca);

        void Eliminar(Guid Id);
    }
}
