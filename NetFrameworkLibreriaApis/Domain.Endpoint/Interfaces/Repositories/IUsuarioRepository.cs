using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> Get();

        Usuario GetById(Guid Id);

        void Create(Usuario usuario);

        void Eliminar(Guid Id);

        void ModificarUsuario(Guid Id, UsuarioDTO modificarUsuario);

    }
}
