using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repository;
        public MaterialService(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public Material CrearMaterial(MaterialDTO nuevaMaterial)
        {
           
            Material newMaterial = new Material()
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

        public Task<List<Material>> GetAll()
        {
            return _repository.Get();
        }

        public Material GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
