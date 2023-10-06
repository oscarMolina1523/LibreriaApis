using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult GetMedida()
        {
            List<UnidadMedidaDTO> medida = _MedidaService.GetAll();
            return Ok(medida);
        }

        [HttpGet]
        public IHttpActionResult GetMedidaById(Guid Id)
        {
            UnidadMedidaDTO medida = _MedidaService.GetById(Id);
            return Ok(medida);
        }

        [HttpPost]
        public IHttpActionResult Create(UnidadMedidaDTO nuevaMedida)
        {
            UnidadMedidaDTO newMedida = _MedidaService.CrearMedida(nuevaMedida);
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
