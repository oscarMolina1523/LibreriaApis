using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IMarcaService
    {
        List<Marca> GetAll();

        Marca GetById(Guid Id);

        Marca CrearMarca(Marca nuevaMarca);

        void EliminarMarca(Guid Id);
    }
}
