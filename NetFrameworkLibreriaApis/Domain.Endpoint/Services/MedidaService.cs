using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class MedidaService : IMedidaService
    {
        private readonly IMedidaRepository _repository;
        public MedidaService(IMedidaRepository repository)
        {
            _repository = repository;
        }

        public UnidadMedida CrearMedida(UnidadMedidaDTO nuevaMedida)
        {
            
            UnidadMedida newMedida = new UnidadMedida()
            {
                Id = Guid.NewGuid(),
                DescripcionMedida = nuevaMedida.DescripcionMedida,
                
            };

            _repository.Create(newMedida);
            return newMedida;
        }

        public async Task<UnidadMedida> EliminarMedida(Guid Id)
        {
            //_repository.Eliminar(Id);
            UnidadMedida medida = await GetById(Id);
            await _repository.Eliminar(medida);
            return medida;
        }

        public Task<List<UnidadMedida>> GetAll()
        {

            return _repository.Get();
        }

        public async Task<UnidadMedida> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
