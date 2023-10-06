using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IEmpleadoRepository
    {
        List<EmpleadoDTO> Get();

        EmpleadoDTO GetById(Guid Id);

        void Create(EmpleadoDTO empleado);

        void Eliminar(Guid Id);

        void ModificarEmpleado(Guid Id, EmpleadoDTO modificarEmpleado);
    }
}
