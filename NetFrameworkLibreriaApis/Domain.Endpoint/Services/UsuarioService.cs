using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public UsuarioDTO CrearUsuario(UsuarioDTO nuevoUsuario)
        {
            
            UsuarioDTO newUsuario = new UsuarioDTO()
            {
                Id = Guid.NewGuid(),
                NombreUsuario=nuevoUsuario.NombreUsuario,
                Contraseña=nuevoUsuario.Contraseña,
                IdEmpleado=nuevoUsuario.IdEmpleado,
                IdRol=nuevoUsuario.IdRol
            };

            _repository.Create(newUsuario);
            return newUsuario;
        }

        public void EliminarUsuario(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public List<UsuarioDTO> GetAll()
        {
           return _repository.Get();
        }

        public void ModificarUsuario(Guid Id, UsuarioDTO cambioUsuario)
        {
            _repository.ModificarUsuario(Id, cambioUsuario);
        }

        public UsuarioDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
