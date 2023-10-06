using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly List<ProveedorDTO> DataAlmacenada = new List<ProveedorDTO>();

        public ProveedorRepository()
        {
            

            var proveedor1 = new ProveedorDTO()
            {
                Id = Guid.NewGuid(),
                NombreProveedor="TIGU",
                Direccion="Managua, de la rotonda 5 cuadras al norte",
                CorreoEletronico="tigu15@gmail.com",
                Telefono = "4895-6251"
            };

            DataAlmacenada.Add(proveedor1);
        }

        public void Create(ProveedorDTO proveedor)
        {
            DataAlmacenada.Add(proveedor);
        }

        public void Eliminar(Guid Id)
        {
            var proveedorAEliminar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (proveedorAEliminar != null)
            {
                DataAlmacenada.Remove(proveedorAEliminar);
            }
            else
            {
                throw new InvalidOperationException("El proveedor no existe.");
            }
        }

        public List<ProveedorDTO> Get()
        {
            return DataAlmacenada;
        }

        public void ModificarProveedor(Guid Id, ProveedorDTO modificarProveedor)
        {
            var proveedorAmodificar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);
            if (proveedorAmodificar != null)
            {
                proveedorAmodificar.NombreProveedor = modificarProveedor.NombreProveedor;
                proveedorAmodificar.Direccion = modificarProveedor.Direccion;
                proveedorAmodificar.CorreoEletronico= modificarProveedor.CorreoEletronico;
                proveedorAmodificar.Telefono = modificarProveedor.Telefono;
            }
            else
            {
                throw new InvalidOperationException("El proveedor no existe.");
            }
        }

        public ProveedorDTO GetById(Guid Id)
        {
            var proveedorAMostrar = DataAlmacenada.FirstOrDefault(c => c.Id == Id);

            if (proveedorAMostrar != null)
            {
                return proveedorAMostrar;
            }
            else
            {
                throw new InvalidOperationException("El proveedor no existe.");
            }

        }
    }
}
