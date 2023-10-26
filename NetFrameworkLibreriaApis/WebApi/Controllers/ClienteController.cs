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
    public class ClienteController : ApiController
    {
        private readonly IClienteService _ClienteService;

        public ClienteController(IClienteService clienteService)
        {
            _ClienteService = clienteService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCliente()
        {
            List<Cliente> cliente = await _ClienteService.GetAll();
            return Ok(cliente);
        }

        [HttpGet]
        public IHttpActionResult GetClienteById(Guid Id)
        {
            Cliente cliente = _ClienteService.GetById(Id);
            return Ok(cliente);
        }

        [HttpPost]
        public IHttpActionResult Create(ClienteDTO nuevoCliente)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            Cliente newCliente = _ClienteService.CrearCliente(nuevoCliente);
            return Ok(newCliente);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _ClienteService.EliminarCliente(Id);
            return Ok("cliente eliminado");
        }

        [HttpPut]
        public IHttpActionResult modificarCliente(Guid Id, ClienteDTO nuevosCliente)
        {
            _ClienteService.ModificarCliente(Id, nuevosCliente);
            return Ok("El cliente ha sido modificado");
        }

    }
}
