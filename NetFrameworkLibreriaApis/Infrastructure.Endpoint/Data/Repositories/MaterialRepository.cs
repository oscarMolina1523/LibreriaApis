using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class MaterialRepository : IMaterialRepository
    {
        private readonly List<MaterialDTO> DataAlmacenada = new List<MaterialDTO>();

        public MaterialRepository()
        {
            

            var material1 = new MaterialDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionMaterial = "Plastico",
                
            };

            DataAlmacenada.Add(material1);
        }

        public void Create(MaterialDTO material)
        {
            DataAlmacenada.Add(material);
        }

        public void Eliminar(Guid Id)
        {
            var materialAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (materialAEliminar != null)
            {
                DataAlmacenada.Remove(materialAEliminar);
            }
            else
            {
                throw new InvalidOperationException("El material no existe.");
            }
        }

        public List<MaterialDTO> Get()
        {
            return DataAlmacenada;
        }

        public MaterialDTO GetById(Guid Id)
        {
            var materialAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (materialAMostrar != null)
            {
                return materialAMostrar;
            }
            else
            {
                throw new InvalidOperationException("El material no existe.");
            }

        }
    }
}
