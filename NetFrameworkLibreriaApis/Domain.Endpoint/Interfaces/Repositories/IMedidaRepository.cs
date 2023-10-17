using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IMedidaRepository
    {
        Task<List<UnidadMedida>> Get();

        UnidadMedida GetById(Guid Id);

        void Create(UnidadMedida medida);

        void Eliminar(Guid Id);
    }
}
