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
    public class ProductoController : ApiController
    {
        private readonly IProductoService _ProductoService;

        public ProductoController(IProductoService productoService) 
        {
            _ProductoService = productoService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetProducto()
        {
            List<Producto> producto = await _ProductoService.GetAll();
            return Ok(producto);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetProductoById(Guid Id)
        {
            Producto producto = await _ProductoService.GetById(Id);
            return Ok(producto);
        }

        [HttpPost]
        public IHttpActionResult Create(ProductoDTO nuevoProducto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            Producto newProducto = _ProductoService.CrearProducto(nuevoProducto);
            return Ok(newProducto);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Eliminar(Guid Id)
        {
            await _ProductoService.EliminarProducto(Id);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> modificarProducto(Guid Id, ProductoDTO nuevosCampos)
        {
            await _ProductoService.ModificarProducto(Id, nuevosCampos);
            return Ok("El producto ha sido modificado");
        }
    }
}
