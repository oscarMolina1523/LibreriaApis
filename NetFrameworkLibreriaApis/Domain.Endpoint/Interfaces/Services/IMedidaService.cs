using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IMedidaService
    {
        Task<List<UnidadMedida>> GetAll();

        UnidadMedida GetById(Guid Id);

        UnidadMedida CrearMedida(UnidadMedidaDTO nuevaMedida);

        void EliminarMedida(Guid Id);
    }
}
