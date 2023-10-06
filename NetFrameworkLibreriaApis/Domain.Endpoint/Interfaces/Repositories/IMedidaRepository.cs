using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IMedidaRepository
    {
        List<UnidadMedidaDTO> Get();

        UnidadMedidaDTO GetById(Guid Id);

        void Create(UnidadMedidaDTO medida);

        void Eliminar(Guid Id);
    }
}
