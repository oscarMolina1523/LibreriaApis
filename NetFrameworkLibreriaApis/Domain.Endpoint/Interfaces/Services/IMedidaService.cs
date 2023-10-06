using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IMedidaService
    {
        List<UnidadMedidaDTO> GetAll();

        UnidadMedidaDTO GetById(Guid Id);

        UnidadMedidaDTO CrearMedida(UnidadMedidaDTO nuevaMedida);

        void EliminarMedida(Guid Id);
    }
}
