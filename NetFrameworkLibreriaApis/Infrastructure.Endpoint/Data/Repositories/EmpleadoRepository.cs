using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class EmpleadoRepository : IEmpleadoRepository
    {

        private readonly List<EmpleadoDTO> DataAlmacenada = new List<EmpleadoDTO>();

        public EmpleadoRepository()
        {
            

            var empleado1 = new EmpleadoDTO()
            {
                Id = Guid.NewGuid(),
                Nombres = "Jorge Isaac",
                Apellidos = "Lopez Ruiz",
                Cedula = "1548-849562-148R",
                Telefono = "4859-8426"
            };

            DataAlmacenada.Add(empleado1);
        }

        public void Create(EmpleadoDTO empleado)
        {
            DataAlmacenada.Add(empleado);
        }

        public void Eliminar(Guid Id)
        {
            var empleadoAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (empleadoAEliminar != null)
            {
                DataAlmacenada.Remove(empleadoAEliminar);
            }
            else
            {
                throw new InvalidOperationException("El empleado no existe.");
            }
        }

        public List<EmpleadoDTO> Get()
        {
            return DataAlmacenada;
        }

        public void ModificarEmpleado(Guid Id, EmpleadoDTO modificarEmpleado)
        {
            var empleadoAModificar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);
            if (empleadoAModificar != null)
            {
                empleadoAModificar.Nombres = modificarEmpleado.Nombres;
                empleadoAModificar.Apellidos = modificarEmpleado.Apellidos;
                empleadoAModificar.Cedula = modificarEmpleado.Cedula;
                empleadoAModificar.Telefono = modificarEmpleado.Telefono;
            }
            else
            {
                throw new InvalidOperationException("El empleado no existe.");
            }
        }

        public EmpleadoDTO GetById(Guid Id)
        {
            var empleadoAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (empleadoAMostrar != null)
            {
                return empleadoAMostrar;
            }
            else
            {
                throw new InvalidOperationException("El empleado no existe.");
            }

        }
    }
}
