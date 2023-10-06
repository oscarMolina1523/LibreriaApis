using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class MedidaService : IMedidaService
    {
        private readonly IMedidaRepository _repository;
        public MedidaService(IMedidaRepository repository)
        {
            _repository = repository;
        }

        public UnidadMedidaDTO CrearMedida(UnidadMedidaDTO nuevaMedida)
        {
            
            UnidadMedidaDTO newMedida = new UnidadMedidaDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionMedida = nuevaMedida.DescripcionMedida,
                
            };

            _repository.Create(newMedida);
            return newMedida;
        }

        public void EliminarMedida(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public List<UnidadMedidaDTO> GetAll()
        {

            return _repository.Get();
        }

        public UnidadMedidaDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
