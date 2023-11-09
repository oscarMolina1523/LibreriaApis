using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApi.Controllers
{
    public class CategoriaController : ApiController
    {
        private readonly ICategoriaService _CategoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _CategoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCategoria()
        {
            List<Categoria> categoria = await _CategoriaService.GetAll();
            return Ok(categoria);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCategoriaById(Guid Id)
        {
            Categoria categoria =await _CategoriaService.GetById(Id);
            return Ok(categoria);
        }

        [HttpPost]
        public IHttpActionResult Create(CategoriaDTO nuevaCategoria)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            Categoria newCategoria = _CategoriaService.crearCategoria(nuevaCategoria);
            return Ok(newCategoria);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Eliminar(Guid Id)
        {
            await _CategoriaService.EliminarCategoria(Id);
            return Ok("categoria eliminada");
        }

    }
}
