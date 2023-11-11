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
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public ProveedorRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(Proveedor proveedor)
        {
            SqlCommand writeCommand = _operationBuilder.From(proveedor)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(Proveedor proveedor)
        {
            SqlCommand writeCommand = _operationBuilder.From(proveedor)
                 .WithOperation(SqlWriteOperation.Delete)
                 .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<Proveedor>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Proveedor>()
                .WithOperation(SqlReadOperation.Select)
                .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Proveedor> proveedores = dt.AsEnumerable().Select(row =>
            new Proveedor
            {
                Id = row.Field<Guid>("ID_PROVEEDOR"),
                Direccion = row.Field<string>("DIRECCION"),
                CorreoElectronico = row.Field<string>("CORREO_ELECTRONICO"),
                Telefono = row.Field<string>("TELEFONO"),
                Descripcion=row.Field<string>("DESCRIPCION_PROVEEDOR")
            }).ToList();

            return proveedores;
        }

        public async Task ModificarProveedor(Proveedor modificarProveedor)
        {
            SqlCommand writeCommand = _operationBuilder.From(modificarProveedor)
                .WithOperation(SqlWriteOperation.Update)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<Proveedor> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Proveedor>()
            .WithOperation(SqlReadOperation.SelectById)
            .WithId(Id)
            .BuildReader();
            Proveedor proveedor = new Proveedor();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                proveedor = new Proveedor
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_PROVEEDOR")),
                    Direccion = reader.GetString(reader.GetOrdinal("DIRECCION")),
                    CorreoElectronico = reader.GetString(reader.GetOrdinal("CORREO_ELECTRONICO")),
                    Telefono = reader.GetString(reader.GetOrdinal("TELEFONO")),
                    Descripcion = reader.GetString(reader.GetOrdinal("DESCRIPCION_PROVEEDOR"))
                };
            }
            reader.Close();
            return proveedor;
        }

        private Proveedor MapEntityFromDataRow(DataRow row)
        {
            return new Proveedor()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_PROVEEDOR"),
                Direccion = _connectionBuilder.GetDataRowValue<string>(row, "DIRECCION"),
                Descripcion = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_PROVEEDOR"),
                CorreoElectronico = _connectionBuilder.GetDataRowValue<string>(row, "CORREO_ELECTRONICO"),
                Telefono = _connectionBuilder.GetDataRowValue<string>(row, "TELEFONO")
            };
        }
    }
}
