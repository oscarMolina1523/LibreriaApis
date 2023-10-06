using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly List<CategoriaDTO> DataAlmacenada=new List<CategoriaDTO>();

        public CategoriaRepository()
        {

            var categoria1 = new CategoriaDTO()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Lacteos",
            };

            DataAlmacenada.Add(categoria1);
        }

        public List<CategoriaDTO> Get()
        {
            return DataAlmacenada;
        }

        public void Create(CategoriaDTO categoria)
        {
            DataAlmacenada.Add(categoria);
        }

        public void Eliminar(Guid Id)
        {
            var categoriaAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (categoriaAEliminar != null)
            {
                DataAlmacenada.Remove(categoriaAEliminar);
            }
            else
            {
                throw new InvalidOperationException("La categoría no existe.");
            }
        }

        public CategoriaDTO GetById(Guid Id)
        {
            var categoriaAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (categoriaAMostrar != null)
            {
                return categoriaAMostrar;
            }
            else
            {
                throw new InvalidOperationException("La categoría no existe.");
            }
            
        }
    }
}
