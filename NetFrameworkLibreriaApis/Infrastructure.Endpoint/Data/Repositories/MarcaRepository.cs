using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class MarcaRepository : IMarcaRepository
    {
        private readonly List<MarcaDTO> DataAlmacenada = new List<MarcaDTO>();

        public MarcaRepository()
        {
            

            var marca1 = new MarcaDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionMarca = "Coca-Cola",
            
            };

            DataAlmacenada.Add(marca1);
        }

        public void Create(MarcaDTO marca)
        {
            DataAlmacenada.Add(marca);
        }

        public void Eliminar(Guid Id)
        {
            var marcaAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (marcaAEliminar != null)
            {
                DataAlmacenada.Remove(marcaAEliminar);
            }
            else
            {
                throw new InvalidOperationException("La marca no existe.");
            }
        }

        public List<MarcaDTO> Get()
        {
            return DataAlmacenada;
        }

        public MarcaDTO GetById(Guid Id)
        {
            var marcaMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (marcaMostrar != null)
            {
                return marcaMostrar;
            }
            else
            {
                throw new InvalidOperationException("La marca no existe.");
            }

        }
    }
}
