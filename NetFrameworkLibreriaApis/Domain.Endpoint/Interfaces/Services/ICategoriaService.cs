using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> GetAll();

        Task<Categoria> GetById(Guid Id);

        Categoria crearCategoria(CategoriaDTO nuevaCategoria);

        Task<Categoria> EliminarCategoria(Guid Id);
    }
}
