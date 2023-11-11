using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public Producto CrearProducto(ProductoDTO nuevoProducto)
        {
           
            Producto newProducto = new Producto()
            {
                Id = Guid.NewGuid(),
                DescripcionProducto = nuevoProducto.DescripcionProducto,
                IdCategoria = nuevoProducto.IdCategoria,
                
            };

            _repository.Create(newProducto);
            return newProducto;
        }

        public async Task<Producto> EliminarProducto(Guid Id)
        {
            //_repository.Eliminar(Id);
            Producto producto = await GetById(Id);
            await _repository.Eliminar(producto);
            return producto;
        }

        public Task<List<Producto>> GetAll()
        {
            return _repository.Get();
        }

        public async Task<Producto> ModificarProducto(Guid Id, ProductoDTO cambioProducto)
        {
            //_repository.ModificarProducto(Id, cambioProducto);
            Producto producto = await GetById(Id);

            Producto newProducto = new Producto
            {
                Id = producto.Id,
                DescripcionProducto=cambioProducto.DescripcionProducto,
                IdCategoria=cambioProducto.IdCategoria
            };

            await _repository.ModificarProducto(newProducto);
            return newProducto;
        }

        public async Task<Producto> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }
    }
}
