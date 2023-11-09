using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IMarcaService
    {
        Task<List<Marca>> GetAll();

        Task<Marca> GetById(Guid Id);

        Marca CrearMarca(MarcaDTO nuevaMarca);

        Task<Marca> EliminarMarca(Guid Id);
    }
}
