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

        public void EliminarCategoria(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public Task<List<Categoria>> GetAll()
        {
            return  _repository.Get();
        }

        public Categoria GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
