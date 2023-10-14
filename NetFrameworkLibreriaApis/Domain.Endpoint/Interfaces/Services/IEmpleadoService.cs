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

        Empleado GetById(Guid Id);

        Empleado CrearEmpleado(EmpleadoDTO nuevoEmpleado);

        void EliminarEmpleado(Guid Id);

        void ModificarEmpleado(Guid Id, EmpleadoDTO cambioEmpleado);
    }
}
