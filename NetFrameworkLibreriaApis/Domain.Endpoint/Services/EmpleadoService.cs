using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _repository;

        public EmpleadoService(IEmpleadoRepository repository)
        {
            _repository = repository;
        }

        public Empleado CrearEmpleado(EmpleadoDTO nuevoEmpleado)
        {
            Empleado newEmpleado = new Empleado()
            {
                Id = Guid.NewGuid(),
                Nombres = nuevoEmpleado.Nombres,
                Apellidos = nuevoEmpleado.Apellidos,
                Cedula=nuevoEmpleado.Cedula,
                Telefono=nuevoEmpleado.Telefono
            };

            _repository.Create(newEmpleado);
            return newEmpleado;
        }

        public void EliminarEmpleado(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public Task<List<Empleado>> GetAll()
        {
            return _repository.Get();
        }

        public void ModificarEmpleado(Guid Id, EmpleadoDTO cambioEmpleado)
        {
            _repository.ModificarEmpleado(Id, cambioEmpleado);
        }

        public Empleado GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
