using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        List<UsuarioDTO> Get();

        UsuarioDTO GetById(Guid Id);

        void Create(UsuarioDTO usuario);

        void Eliminar(Guid Id);

        void ModificarUsuario(Guid Id, UsuarioDTO modificarUsuario);

    }
}
