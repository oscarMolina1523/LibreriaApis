﻿using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public ProductoDTO CrearProducto(ProductoDTO nuevoProducto)
        {
           
            ProductoDTO newProducto = new ProductoDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionProducto = nuevoProducto.DescripcionProducto,
                IdCategoria = nuevoProducto.IdCategoria,
                
            };

            _repository.Create(newProducto);
            return newProducto;
        }

        public void EliminarProducto(Guid Id)
        {
            _repository.Eliminar(Id);
        }

        public List<ProductoDTO> GetAll()
        {
            return _repository.Get();
        }

        public void ModificarProducto(Guid Id, ProductoDTO cambioProducto)
        {
            _repository.ModificarProducto(Id, cambioProducto);
        }

        public ProductoDTO GetById(Guid Id)
        {
            return _repository.GetById(Id);
        }
    }
}
