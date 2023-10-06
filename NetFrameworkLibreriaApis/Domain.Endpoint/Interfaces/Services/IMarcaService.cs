using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IMarcaService
    {
        List<MarcaDTO> GetAll();

        MarcaDTO GetById(Guid Id);

        MarcaDTO CrearMarca(MarcaDTO nuevaMarca);

        void EliminarMarca(Guid Id);
    }
}
