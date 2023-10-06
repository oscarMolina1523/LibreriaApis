using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IEmpleadoService
    {
        List<EmpleadoDTO> GetAll();

        EmpleadoDTO GetById(Guid Id);

        EmpleadoDTO CrearEmpleado(EmpleadoDTO nuevoEmpleado);

        void EliminarEmpleado(Guid Id);

        void ModificarEmpleado(Guid Id, EmpleadoDTO cambioEmpleado);
    }
}
