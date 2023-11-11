using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IHttpActionResult> GetUsuarioById(Guid Id)
        {
            Usuario usuario = await _UsuarioService.GetById(Id);
            return Ok(usuario);
        }

        [HttpPost]
        public IHttpActionResult Create(UsuarioDTO nuevoUsuario)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            Usuario newUsuario = _UsuarioService.CrearUsuario(nuevoUsuario);
            return Ok(newUsuario);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Eliminar(Guid Id)
        {
            await _UsuarioService.EliminarUsuario(Id);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> modificarUsuario(Guid Id, UsuarioDTO nuevosCampos)
        {
            await _UsuarioService.ModificarUsuario(Id, nuevosCampos);
            return Ok("El usuario ha sido modificado");
        }

    }
}
