using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IUsuarioService
    {
        List<UsuarioDTO> GetAll();

        UsuarioDTO GetById(Guid Id);

        UsuarioDTO CrearUsuario(UsuarioDTO nuevoUsuario);

        void EliminarUsuario(Guid Id);

        void ModificarUsuario(Guid Id, UsuarioDTO cambioUsuario);
    }
}
