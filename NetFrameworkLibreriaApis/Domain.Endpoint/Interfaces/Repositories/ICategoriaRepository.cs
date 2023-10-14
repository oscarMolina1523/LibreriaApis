using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> Get();

        Categoria GetById(Guid Id);

        void Create(Categoria categoria);

        void Eliminar(Guid Id);
    }
}
