using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public Categoria crearCategoria(CategoriaDTO nuevaCategoria)
        {
            Categoria newCategoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                Descripcion = nuevaCategoria.Descripcion,
            };

            _repository.Create(newCategoria);
            return newCategoria;
        }

        public async Task<Categoria> EliminarCategoria(Guid Id)
        {
            //_repository.Eliminar(Id);

            Categoria categoria = await GetById(Id);
            await _repository.Eliminar(categoria);
            return categoria;
        }

        public Task<List<Categoria>> GetAll()
        {
            return  _repository.Get();
        }

        public async Task<Categoria> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
