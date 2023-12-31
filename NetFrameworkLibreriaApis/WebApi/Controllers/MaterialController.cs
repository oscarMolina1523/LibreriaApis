﻿using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetMaterial()
        {
            List<Material> material = await _materialService.GetAll();
            return Ok(material);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMaterialById(Guid Id)
        {
            Material material =await _materialService.GetById(Id);
            return Ok(material);
        }

        [HttpPost]
        public IHttpActionResult crearMaterial(MaterialDTO nuevoMaterial)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            Material newMaterial = _materialService.CrearMaterial(nuevoMaterial);
            return Ok(newMaterial);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Eliminar(Guid Id)
        {
            await _materialService.EliminarMaterial(Id);
            return Ok();
        }
    }
}
