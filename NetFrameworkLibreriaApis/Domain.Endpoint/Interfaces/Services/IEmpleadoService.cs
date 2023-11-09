using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IEmpleadoService  
    {
        Task<List<Empleado>> GetAll();

        Task<Empleado> GetById(Guid Id);

        Empleado CrearEmpleado(EmpleadoDTO nuevoEmpleado);

        Task<Empleado> EliminarEmpleado(Guid Id);

        Task<Empleado> ModificarEmpleado(Guid Id, EmpleadoDTO cambioEmpleado);
    }
}
