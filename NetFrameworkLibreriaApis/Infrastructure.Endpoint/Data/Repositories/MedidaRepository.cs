using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class MedidaRepository : IMedidaRepository
    {
        private readonly List<UnidadMedidaDTO> DataAlmacenada = new List<UnidadMedidaDTO>();

        public MedidaRepository()
        {
            

            var medida1 = new UnidadMedidaDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionMedida = "Unidades",
                
            };

            DataAlmacenada.Add(medida1);
        }

        public void Create(UnidadMedidaDTO medida)
        {
            DataAlmacenada.Add(medida);
        }

        public void Eliminar(Guid Id)
        {
            var medidaAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (medidaAEliminar != null)
            {
                DataAlmacenada.Remove(medidaAEliminar);
            }
            else
            {
                throw new InvalidOperationException("La medida no existe.");
            }
        }

        public List<UnidadMedidaDTO> Get()
        {
            return DataAlmacenada;
        }

        public UnidadMedidaDTO GetById(Guid Id)
        {
            var medidaAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (medidaAMostrar != null)
            {
                return medidaAMostrar;
            }
            else
            {
                throw new InvalidOperationException("La unidad de medida no existe.");
            }

        }
    }
}
