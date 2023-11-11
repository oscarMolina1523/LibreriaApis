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

        public async Task<Usuario> EliminarUsuario(Guid Id)
        {
            //_repository.Eliminar(Id);
            Usuario usuario = await GetById(Id);
            await _repository.Eliminar(usuario);
            return usuario;
        }

        public Task<List<Usuario>> GetAll()
        {
          return _repository.Get(); 
        }

        public async Task<Usuario> ModificarUsuario(Guid Id, UsuarioDTO cambioUsuario)
        {
            //_repository.ModificarUsuario(Id, cambioUsuario);
            Usuario usuario = await GetById(Id);

            Usuario newUsuario = new Usuario
            {
                Id = usuario.Id,
                NombreUsuario=cambioUsuario.NombreUsuario,
                Contraseña=cambioUsuario.Contraseña,
                IdEmpleado=cambioUsuario.IdEmpleado,
                IdRol=cambioUsuario.IdRol,
                
            };

            await _repository.ModificarUsuario(newUsuario);
            return newUsuario;
        }

        public async Task<Usuario> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
