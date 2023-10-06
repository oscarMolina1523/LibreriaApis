using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly List<UsuarioDTO> DataAlmacenada = new List<UsuarioDTO>();

        public UsuarioRepository()
        {
            
            var usuario1 = new UsuarioDTO()
            {
                Id = Guid.NewGuid(),
                NombreUsuario="Jorge15",
                Contraseña="15944895",
                IdEmpleado=Guid.NewGuid(),
                IdRol=Guid.NewGuid(),
            };

            DataAlmacenada.Add(usuario1);
        }

        public void Create(UsuarioDTO usuario)
        {
            DataAlmacenada.Add(usuario);
        }

        public void Eliminar(Guid Id)
        {
            var usuarioAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (usuarioAEliminar != null)
            {
                DataAlmacenada.Remove(usuarioAEliminar);
            }
            else
            {
                throw new InvalidOperationException("El usuario no existe.");
            }
        }

        public List<UsuarioDTO> Get()
        {
            return DataAlmacenada;
        }

        public void ModificarUsuario(Guid Id, UsuarioDTO modificarUsuario)
        {
            var usuarioAModificar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);
            if (usuarioAModificar != null)
            {
                usuarioAModificar.NombreUsuario = modificarUsuario.NombreUsuario;
                usuarioAModificar.Contraseña = modificarUsuario.Contraseña;
                usuarioAModificar.IdEmpleado = modificarUsuario.IdEmpleado;
                usuarioAModificar.IdRol = modificarUsuario.IdRol;
            }
            else
            {
                throw new InvalidOperationException("El usuario no existe.");
            }
        }

        public UsuarioDTO GetById(Guid Id)
        {
            var usuarioAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (usuarioAMostrar != null)
            {
                return usuarioAMostrar;
            }
            else
            {
                throw new InvalidOperationException("El usuario no existe.");
            }

        }
    }
}
