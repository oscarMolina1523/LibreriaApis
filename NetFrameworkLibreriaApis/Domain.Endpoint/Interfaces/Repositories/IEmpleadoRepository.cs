using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IEmpleadoRepository 
    {
        Task<List<Empleado>> Get();

        Task<Empleado> GetById(Guid Id); 

        void Create(Empleado empleado);

        Task Eliminar(Empleado empleado);

        Task ModificarEmpleado(Empleado modificarEmpleado);
    }
}
