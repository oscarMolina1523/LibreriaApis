using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repository;
        public MarcaService(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public Marca CrearMarca(MarcaDTO nuevaMarca)
        {
            
            Marca newMarca = new Marca()
            {
                Id = Guid.NewGuid(),
                DescripcionMarca = nuevaMarca.DescripcionMarca,
               
            };

            _repository.Create(newMarca);
            return newMarca;
        }

        public async Task<Marca> EliminarMarca(Guid Id)
        {
            //_repository.Eliminar(Id);
            Marca marca = await GetById(Id);
            await _repository.Eliminar(marca);
            return marca;
        }

        public Task<List<Marca>> GetAll()
        {
            return _repository.Get();
        }

        public async Task<Marca> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
