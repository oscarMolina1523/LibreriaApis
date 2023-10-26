using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public Usuario CrearUsuario(UsuarioDTO nuevoUsuario)
        {
            
            Usuario newUsuario = new Usuario()
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

        public Task<List<Usuario>> GetAll()
        {
          return _repository.Get(); 
        }

        public void ModificarUsuario(Guid Id, UsuarioDTO cambioUsuario)
        {
            _repository.ModificarUsuario(Id, cambioUsuario);
        }

        public Usuario GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
