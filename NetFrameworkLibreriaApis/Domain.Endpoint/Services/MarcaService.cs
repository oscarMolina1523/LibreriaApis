using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repository;
        public MarcaService(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public Marca CrearMarca(Marca nuevaMarca)
        {
            
            Marca newMarca = new Marca()
            {
                Id = Guid.NewGuid(),
                DescripcionMarca = nuevaMarca.DescripcionMarca,
               
            };

            _repository.Create(newMarca);
            return newMarca;
        }

        public void EliminarMarca(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public List<Marca> GetAll()
        {
            return _repository.Get();
        }

        public Marca GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
