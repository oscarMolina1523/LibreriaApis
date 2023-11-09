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
    public class DetalleProductoRepository : IDetalleProductoRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public DetalleProductoRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(DetalleProducto detalleProducto)
        {
            SqlCommand writeCommand = _operationBuilder.From(detalleProducto)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(DetalleProducto detalleProducto)
        {
            SqlCommand writeCommand = _operationBuilder.From(detalleProducto)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<DetalleProducto>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<DetalleProducto>()
               .WithOperation(SqlReadOperation.Select)
               .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<DetalleProducto> detalle = dt.AsEnumerable().Select(row =>
            new DetalleProducto
            {
                Id = row.Field<Guid>("ID_DETALLE_PRODUCTO"),
                IdProducto = row.Field<Guid>("ID_PRODUCTO"),
                IdMarca = row.Field<Guid>("ID_MARCA"),
                IdUnidadMedida = row.Field<Guid>("ID_UNIDAD_MEDIDA"),
                IdMaterial = row.Field<Guid>("ID_MATERIAL")

            }).ToList();

            return detalle;
        }

        public async Task ModificarDetalle(DetalleProducto modificarDetalle)
        {
            SqlCommand writeCommand = _operationBuilder.From(modificarDetalle)
               .WithOperation(SqlWriteOperation.Update)
               .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<DetalleProducto> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<DetalleProducto>()
            .WithOperation(SqlReadOperation.SelectById)
            .WithId(Id)
            .BuildReader();
            DetalleProducto detalle = new DetalleProducto();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                detalle = new DetalleProducto
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_DETALLE_PRODUCTO")),
                    IdProducto=reader.GetGuid(reader.GetOrdinal("ID_PRODUCTO")),
                    IdMarca = reader.GetGuid(reader.GetOrdinal("ID_MARCA")),
                    IdUnidadMedida = reader.GetGuid(reader.GetOrdinal("ID_UNIDAD_MEDIDA")),
                    IdMaterial = reader.GetGuid(reader.GetOrdinal("ID_MATERIAL")),
                };
            }
            reader.Close();
            return detalle;
        }

        private DetalleProducto MapEntityFromDataRow(DataRow row)
        {
            return new DetalleProducto()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_DETALLE_PRODUCTO"),
                IdProducto = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_PRODUCTO"),
                IdMarca = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_MARCA"),
                IdMaterial = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_MATERIAL"),
                IdUnidadMedida = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_UNIDAD_MEDIDA"),
            };
        }

        
    }
}
