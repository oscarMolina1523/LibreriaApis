using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAll();

        Task<Usuario> GetById(Guid Id);

        Usuario CrearUsuario(UsuarioDTO nuevoUsuario);

        Task<Usuario> EliminarUsuario(Guid Id);

        Task<Usuario> ModificarUsuario(Guid Id, UsuarioDTO cambioUsuario);
    }
}
