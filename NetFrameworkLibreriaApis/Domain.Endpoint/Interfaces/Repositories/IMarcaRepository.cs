using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IMarcaRepository
    {
        List<Marca> Get();

        Marca GetById(Guid Id);

        void Create(Marca marca);

        void Eliminar(Guid Id);
    }
}
