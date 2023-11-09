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

        public async Task<Empleado> EliminarEmpleado(Guid Id)
        {
            //_repository.Eliminar(Id);
            Empleado empleado = await GetById(Id);
            await _repository.Eliminar(empleado);
            return empleado;
        }

        public Task<List<Empleado>> GetAll()
        {
            return _repository.Get();
        }

        public async Task<Empleado> ModificarEmpleado(Guid Id, EmpleadoDTO cambioEmpleado)
        {
            //_repository.ModificarEmpleado(Id, cambioEmpleado);
            Empleado empleado = await GetById(Id);

            Empleado newEmpleado = new Empleado
            {
                Id = empleado.Id,
                Nombres = cambioEmpleado.Nombres,
                Apellidos = cambioEmpleado.Apellidos,
                Cedula = cambioEmpleado.Cedula,
                Telefono = cambioEmpleado.Telefono
            };

            await _repository.ModificarEmpleado(newEmpleado);
            return newEmpleado;
        }

        public async Task<Empleado> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
