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
        private readonly List<Marca> DataAlmacenada = new List<Marca>();

        public MarcaRepository()
        {
            

            var marca1 = new Marca()
            {
                Id = Guid.NewGuid(),
                DescripcionMarca = "Coca-Cola",
            
            };

            DataAlmacenada.Add(marca1);
        }

        public void Create(Marca marca)
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

        public List<Marca> Get()
        {
            return DataAlmacenada;
        }

        public Marca GetById(Guid Id)
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
