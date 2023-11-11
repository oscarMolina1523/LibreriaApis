using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Builders;
using Infrastructure.Endpoint.Interfaces;
using System.Collections.Generic;
using System;
using System.Data;

namespace Infrastructure.Endpoint.Services
{
    public class EntitiesService : IEntitiesService
    {
        private Dictionary<Type, SqlEntitySettings> entities = new Dictionary<Type, SqlEntitySettings>();

        public EntitiesService()
        {
            BuildEntities();
        }

        public SqlEntitySettings GetSettings<TEntity>() where TEntity : BaseEntity
        {
            if (!entities.ContainsKey(typeof(TEntity))) throw new ArgumentOutOfRangeException(nameof(TEntity), "Entidad no encontrada");

            return entities[typeof(TEntity)];
        }

        private void BuildEntities()
        {
            SqlEntitySettings categoriaSettings = GetCategoriaSettings();
            SqlEntitySettings clientesSettings = GetClienteSettings();
            SqlEntitySettings detalleSettings = GetDetalleSettings();
            SqlEntitySettings empleadoSettings = GetEmpleadoSettings();
            SqlEntitySettings marcaSettings = GetMarcaSettings();
            SqlEntitySettings medidaSettings = GetMedidaSettings();
            SqlEntitySettings materialSettings = GetMaterialSettings();
            SqlEntitySettings productoSettings = GetProductoSettings();
            SqlEntitySettings proveedorSettings = GetProveedorSettings();
            entities.Add(typeof(Categoria), categoriaSettings);
            entities.Add(typeof(Cliente), clientesSettings);
            entities.Add(typeof(DetalleProducto), detalleSettings);
            entities.Add(typeof(Empleado), empleadoSettings);
            entities.Add(typeof(Marca), marcaSettings);
            entities.Add(typeof(UnidadMedida), medidaSettings);
            entities.Add(typeof(Material), materialSettings);
            entities.Add(typeof(Producto), productoSettings);
            entities.Add(typeof(Proveedor), proveedorSettings);
        }

        private SqlEntitySettings GetCategoriaSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_CATEGORIA", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "DESCRIPCION_CATEGORIA", DomainName = "Descripcion", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "CATEGORIA",
                Columns = columns
            };
        }

        private SqlEntitySettings GetClienteSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_CLIENTE", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "NOMBRES", DomainName = "Nombres", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "CEDULA", DomainName = "Cedula", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "TELEFONO", DomainName = "Telefono", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "CLIENTE",
                Columns = columns
            };
        }

        private SqlEntitySettings GetDetalleSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_DETALLE_PRODUCTO", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "ID_PRODUCTO", DomainName = "IdProducto", SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "ID_MARCA", DomainName = "IdMarca", SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "ID_UNIDAD_MEDIDA", DomainName = "IdUnidadMedida", SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "ID_MATERIAL", DomainName = "IdMaterial", SqlDbType = SqlDbType.UniqueIdentifier },
            };

            return new SqlEntitySettings()
            {
                TableName = "DETALLE_PRODUCTO",
                Columns = columns
            };
        }

        private SqlEntitySettings GetEmpleadoSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_EMPLEADO", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "NOMBRES", DomainName = "Nombres", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "APELLIDOS", DomainName = "Apellidos", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "CEDULA", DomainName = "Cedula", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "TELEFONO", DomainName = "Telefono", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "EMPLEADO",
                Columns = columns
            };
        }

        private SqlEntitySettings GetMarcaSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_MARCA", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "DESCRIPCION_MARCA", DomainName = "DescripcionMarca", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "MARCA",
                Columns = columns
            };
        }

        private SqlEntitySettings GetMedidaSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_UNIDAD_MEDIDA", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "DESCRIPCION_MEDIDA", DomainName = "DescripcionMedida", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "UNIDAD_MEDIDA",
                Columns = columns
            };
        }

        private SqlEntitySettings GetMaterialSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_MATERIAL", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "DESCRIPCION_MATERIAL", DomainName = "DescripcionMaterial", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "MATERIAL",
                Columns = columns
            };
        }

        private SqlEntitySettings GetProductoSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_PRODUCTO", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "ID_CATEGORIA", DomainName = "IdCategoria", SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "DESCRIPCION_PRODUCTO", DomainName = "DescripcionProducto", SqlDbType = SqlDbType.NVarChar },
            };

            return new SqlEntitySettings()
            {
                TableName = "PRODUCTO",
                Columns = columns
            };
        }

        private SqlEntitySettings GetProveedorSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_PROVEEDOR", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "DIRECCION", DomainName = "Direccion", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "CORREO_ELECTRONICO", DomainName = "CorreoElectronico", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "TELEFONO", DomainName = "Telefono", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "DESCRIPCION_PROVEEDOR", DomainName = "Descripcion", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "PROVEEDOR",
                Columns = columns
            };
        }
    }
}
