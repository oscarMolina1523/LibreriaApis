using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public CategoriaDTO crearCategoria(CategoriaDTO nuevaCategoria)
        {
            CategoriaDTO newCategoria = new CategoriaDTO()
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

        public List<CategoriaDTO> GetAll()
        {
            return _repository.Get();
        }

        public CategoriaDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
