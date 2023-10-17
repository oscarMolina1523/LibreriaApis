using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _UsuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _UsuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUsuario()
        {
            List<Usuario> usuario = await _UsuarioService.GetAll();
            return Ok(usuario);
        }

        [HttpGet]
        public IHttpActionResult GetUsuarioById(Guid Id)
        {
            Usuario usuario = _UsuarioService.GetById(Id);
            return Ok(usuario);
        }

        [HttpPost]
        public IHttpActionResult Create(UsuarioDTO nuevoUsuario)
        {
            Usuario newUsuario = _UsuarioService.CrearUsuario(nuevoUsuario);
            return Ok(newUsuario);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _UsuarioService.EliminarUsuario(Id);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult modificarUsuario(Guid Id, UsuarioDTO nuevosCampos)
        {
            _UsuarioService.ModificarUsuario(Id, nuevosCampos);
            return Ok("El usuario ha sido modificado");
        }

    }
}
