using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly List<ProductoDTO> DataAlmacenada = new List<ProductoDTO>();

        public ProductoRepository()
        {
            

            var producto1 = new ProductoDTO()
            {
                Id = Guid.NewGuid(),
                DescripcionProducto = "Libro de Matematicas de 4 año",
                IdCategoria = Guid.NewGuid()
            };

            DataAlmacenada.Add(producto1);
        }

        public void Create(ProductoDTO producto)
        {
            DataAlmacenada.Add(producto);
        }

        public void Eliminar(Guid Id)
        {
            var productoAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (productoAEliminar != null)
            {
                DataAlmacenada.Remove(productoAEliminar);
            }
            else
            {
                throw new InvalidOperationException("El producto no existe.");
            }
        }

        public List<ProductoDTO> Get()
        {
            return DataAlmacenada;
        }

        public void ModificarProducto(Guid Id, ProductoDTO modificarProducto)
        {
            var productoAModificar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);
            if (productoAModificar != null)
            {
                productoAModificar.DescripcionProducto = modificarProducto.DescripcionProducto;
                productoAModificar.IdCategoria = modificarProducto.IdCategoria;
            }
            else
            {
                throw new InvalidOperationException("El producto no existe.");
            }
        }

        public ProductoDTO GetById(Guid Id)
        {
            var productoAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (productoAMostrar != null)
            {
                return productoAMostrar;
            }
            else
            {
                throw new InvalidOperationException("El producto no existe.");
            }

        }
    }
}
