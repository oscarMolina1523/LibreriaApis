using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class MaterialController : ApiController
    {
        private readonly IMaterialService _materialService;
        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public IHttpActionResult GetMaterial()
        {
            List<MaterialDTO> material = _materialService.GetAll();
            return Ok(material);
        }

        [HttpGet]
        public IHttpActionResult GetMaterialById(Guid Id)
        {
            MaterialDTO material = _materialService.GetById(Id);
            return Ok(material);
        }

        [HttpPost]
        public IHttpActionResult crearMaterial(MaterialDTO nuevoMaterial)
        {
            MaterialDTO newMaterial = _materialService.CrearMaterial(nuevoMaterial);
            return Ok(newMaterial);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _materialService.EliminarMaterial(Id);
            return Ok();
        }
    }
}
