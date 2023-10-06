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

        public MarcaDTO CrearMarca(MarcaDTO nuevaMarca)
        {
            
            MarcaDTO newMarca = new MarcaDTO()
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

        public List<MarcaDTO> GetAll()
        {
            return _repository.Get();
        }

        public MarcaDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
