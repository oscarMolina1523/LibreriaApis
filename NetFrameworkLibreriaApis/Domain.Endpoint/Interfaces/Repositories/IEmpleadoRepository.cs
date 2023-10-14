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

        Empleado GetById(Guid Id);

        void Create(Empleado empleado);

        void Eliminar(Guid Id);

        void ModificarEmpleado(Guid Id, EmpleadoDTO modificarEmpleado);
    }
}
