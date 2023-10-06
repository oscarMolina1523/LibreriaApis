using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface ICategoriaService
    {
        List<CategoriaDTO> GetAll();

        CategoriaDTO GetById(Guid Id);

        CategoriaDTO crearCategoria(CategoriaDTO nuevaCategoria);

        void EliminarCategoria(Guid Id);
    }
}
