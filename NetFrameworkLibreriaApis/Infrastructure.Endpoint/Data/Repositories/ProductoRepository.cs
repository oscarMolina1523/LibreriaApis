using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Endpoint.Dtos;
using Infrastructure.Endpoint.Interfaces;
using static Infrastructure.Endpoint.Builders.SqlOperations;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public ProductoRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(Producto producto)
        {
            SqlCommand writeCommand = _operationBuilder.From(producto)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(Producto producto)
        {
            SqlCommand writeCommand = _operationBuilder.From(producto)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<Producto>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Producto>()
               .WithOperation(SqlReadOperation.Select)
               .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Producto> productos = dt.AsEnumerable().Select(row =>
            new Producto
            {
                Id = row.Field<Guid>("ID_PRODUCTO"),
                IdCategoria = row.Field<Guid>("ID_CATEGORIA"),
                DescripcionProducto = row.Field<string>("DESCRIPCION_PRODUCTO"),
                
            }).ToList();

            return productos;
        }

        public async Task ModificarProducto(Producto modificarProducto)
        {
            SqlCommand writeCommand = _operationBuilder.From(modificarProducto)
               .WithOperation(SqlWriteOperation.Update)
               .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<Producto> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Producto>()
             .WithOperation(SqlReadOperation.SelectById)
             .WithId(Id)
             .BuildReader();
            Producto producto = new Producto();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                producto = new Producto
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_PRODUCTO")),
                    IdCategoria = reader.GetGuid(reader.GetOrdinal("ID_CATEGORIA")),
                    DescripcionProducto = reader.GetString(reader.GetOrdinal("DESCRIPCION_PRODUCTO")),
                };
            }
            reader.Close();
            return producto;
        }

        private Producto MapEntityFromDataRow(DataRow row)
        {
            return new Producto()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_PRODUCTO"),
                DescripcionProducto = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_PRODUCTO"),
                IdCategoria = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_CATEGORIA"),
            };
        }
    }
}
