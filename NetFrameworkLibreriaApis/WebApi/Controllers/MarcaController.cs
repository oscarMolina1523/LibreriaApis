using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class MarcaController : ApiController
    {
        private readonly IMarcaService _MarcaService;

        public MarcaController(IMarcaService MarcaService)
        {
            _MarcaService = MarcaService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMarca()
        {
            List<Marca> marca = await _MarcaService.GetAll();
            return Ok(marca);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMarcaById(Guid Id)
        {
            Marca marca = await _MarcaService.GetById(Id);
            return Ok(marca);
        }

        [HttpPost]
        public IHttpActionResult Create(MarcaDTO nuevaMarca)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            Marca newMarca = _MarcaService.CrearMarca(nuevaMarca);
            return Ok(newMarca);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Eliminar(Guid Id)
        {
           await  _MarcaService.EliminarMarca(Id);
            return Ok("la marca ha sido eliminada");
        }
    }
}
