using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> Get();

        Task<Marca> GetById(Guid Id);

        void Create(Marca marca);

        Task Eliminar(Marca marca);
    }
}
