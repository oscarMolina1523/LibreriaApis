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

        Usuario GetById(Guid Id);

        Usuario CrearUsuario(UsuarioDTO nuevoUsuario);

        void EliminarUsuario(Guid Id);

        void ModificarUsuario(Guid Id, UsuarioDTO cambioUsuario);
    }
}
