using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class RolRepository : IRolRepository
    {
        private readonly List<RolDTO> DataAlmacenada = new List<RolDTO>();

        public RolRepository()
        {
            

            var rol1 = new RolDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionRol = "Administrador",
                
            };

            DataAlmacenada.Add(rol1);
        }

        public void Create(RolDTO rol)
        {
            DataAlmacenada.Add(rol);
        }

        public void Eliminar(Guid Id)
        {
            var rolAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (rolAEliminar != null)
            {
                DataAlmacenada.Remove(rolAEliminar);
            }
            else
            {
                throw new InvalidOperationException("La medida no existe.");
            }
        }

        public List<RolDTO> Get()
        {
            return DataAlmacenada;
        }

        public RolDTO GetById(Guid Id)
        {
            var rolAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (rolAMostrar != null)
            {
                return rolAMostrar;
            }
            else
            {
                throw new InvalidOperationException("El rol no existe.");
            }

        }
    }
}
