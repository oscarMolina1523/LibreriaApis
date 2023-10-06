﻿using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DetalleController : ApiController
    {
        private readonly IDetalleService _DetalleService;

        public DetalleController(IDetalleService detalleService)
        {
            _DetalleService = detalleService;
        }

        [HttpGet]
        public IHttpActionResult GetDetalle()
        {
            List<DetalleProductoDTO> detalle = _DetalleService.GetAll();
            return Ok(detalle);
        }

        [HttpGet]
        public IHttpActionResult GetDetalleById(Guid Id)
        {
            DetalleProductoDTO detalle = _DetalleService.GetById(Id);
            return Ok(detalle);
        }

        [HttpPost]
        public IHttpActionResult Create(DetalleProductoDTO nuevoDetalle)
        {
            DetalleProductoDTO newDetalle = _DetalleService.CrearDetalle(nuevoDetalle);
            return Ok(newDetalle);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _DetalleService.EliminarDetalle(Id);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult modificarDetalle(Guid Id, DetalleProductoDTO nuevosCampos)
        {
            _DetalleService.ModificarDetalle(Id, nuevosCampos);
            return Ok("El detalle de producto ha sido modificado");
        }


    }
}
