using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ProductoController : ApiController
    {
        private readonly IProductoService _ProductoService;

        public ProductoController(IProductoService productoService)
        {
            _ProductoService = productoService;
        }

        [HttpGet]
        public IHttpActionResult GetProducto()
        {
            List<ProductoDTO> producto = _ProductoService.GetAll();
            return Ok(producto);
        }

        [HttpGet]
        public IHttpActionResult GetProductoById(Guid Id)
        {
            ProductoDTO producto = _ProductoService.GetById(Id);
            return Ok(producto);
        }

        [HttpPost]
        public IHttpActionResult Create(ProductoDTO nuevoProducto)
        {
            ProductoDTO newProducto = _ProductoService.CrearProducto(nuevoProducto);
            return Ok(newProducto);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _ProductoService.EliminarProducto(Id);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult modificarProducto(Guid Id, ProductoDTO nuevosCampos)
        {
            _ProductoService.ModificarProducto(Id, nuevosCampos);
            return Ok("El producto ha sido modificado");
        }
    }
}
