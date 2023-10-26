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
    public class MedidaController : ApiController
    {
        private readonly IMedidaService _MedidaService;

        public MedidaController(IMedidaService MedidaService)
        {
            _MedidaService = MedidaService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMedida()
        {
            List<UnidadMedida> medida =await _MedidaService.GetAll();
            return Ok(medida);
        }

        [HttpGet]
        public IHttpActionResult GetMedidaById(Guid Id)
        {
            UnidadMedida medida = _MedidaService.GetById(Id);
            return Ok(medida);
        }

        [HttpPost]
        public IHttpActionResult Create(UnidadMedidaDTO nuevaMedida)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            UnidadMedida newMedida = _MedidaService.CrearMedida(nuevaMedida);
            return Ok(newMedida);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _MedidaService.EliminarMedida(Id);
            return Ok();
        }
    }
}
