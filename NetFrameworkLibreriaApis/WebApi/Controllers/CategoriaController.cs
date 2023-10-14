using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult GetCategoriaById(Guid Id)
        {
            Categoria categoria = _CategoriaService.GetById(Id);
            return Ok(categoria);
        }

        [HttpPost]
        public IHttpActionResult Create(CategoriaDTO nuevaCategoria)
        {
            Categoria newCategoria = _CategoriaService.crearCategoria(nuevaCategoria);
            return Ok(newCategoria);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
             _CategoriaService.EliminarCategoria(Id);
            return Ok("categoria eliminada");
        }

    }
}
