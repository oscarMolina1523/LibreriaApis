using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repository;
        public MaterialService(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public MaterialDTO CrearMaterial(MaterialDTO nuevaMaterial)
        {
           
            MaterialDTO newMaterial = new MaterialDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionMaterial = nuevaMaterial.DescripcionMaterial,
              
            };

            _repository.Create(newMaterial);
            return newMaterial;
        }

        public void EliminarMaterial(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public List<MaterialDTO> GetAll()
        {
            return _repository.Get();
        }

        public MaterialDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
