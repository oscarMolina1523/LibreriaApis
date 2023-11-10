using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IMedidaRepository
    {
        Task<List<UnidadMedida>> Get();

        Task<UnidadMedida> GetById(Guid Id);

        void Create(UnidadMedida medida);

        Task Eliminar(UnidadMedida medida);
    }
}
