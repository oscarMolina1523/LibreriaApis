using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        List<CategoriaDTO> Get();

        CategoriaDTO GetById(Guid Id);

        void Create(CategoriaDTO categoria);

        void Eliminar(Guid Id);
    }
}
