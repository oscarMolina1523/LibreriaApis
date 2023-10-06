using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult GetMarca()
        {
            List<MarcaDTO> marca = _MarcaService.GetAll();
            return Ok(marca);
        }

        [HttpGet]
        public IHttpActionResult GetMarcaById(Guid Id)
        {
            MarcaDTO marca = _MarcaService.GetById(Id);
            return Ok(marca);
        }

        [HttpPost]
        public IHttpActionResult Create(MarcaDTO nuevaMarca)
        {
            MarcaDTO newMarca = _MarcaService.CrearMarca(nuevaMarca);
            return Ok(newMarca);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _MarcaService.EliminarMarca(Id);
            return Ok();
        }
    }
}
