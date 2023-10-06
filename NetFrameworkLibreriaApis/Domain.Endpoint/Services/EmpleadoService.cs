using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _repository;

        public EmpleadoService(IEmpleadoRepository repository)
        {
            _repository = repository;
        }

        public EmpleadoDTO CrearEmpleado(EmpleadoDTO nuevoEmpleado)
        {
            EmpleadoDTO newEmpleado = new EmpleadoDTO()
            {
                Id = Guid.NewGuid(),
                Nombres = nuevoEmpleado.Nombres,
                Apellidos=nuevoEmpleado.Apellidos,
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

        public List<EmpleadoDTO> GetAll()
        {
            return _repository.Get();
        }

        public void ModificarEmpleado(Guid Id, EmpleadoDTO cambioEmpleado)
        {
            _repository.ModificarEmpleado(Id, cambioEmpleado);
        }

        public EmpleadoDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
