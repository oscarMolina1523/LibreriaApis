using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Infrastructure.Endpoint.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Endpoint.Builders.SqlOperations;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class RolRepository : IRolRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public RolRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(Rol rol)
        {
            SqlCommand writeCommand = _operationBuilder.From(rol)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(Rol rol)
        {
            SqlCommand writeCommand = _operationBuilder.From(rol)
               .WithOperation(SqlWriteOperation.Delete)
               .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<Rol>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Rol>()
                .WithOperation(SqlReadOperation.Select)
                .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Rol> roles = dt.AsEnumerable().Select(row =>
            new Rol
            {
                Id = row.Field<Guid>("ID_ROLES"),
                DescripcionRol = row.Field<string>("DESCRIPCION_ROLES"),
            }).ToList();

            return roles;
        }

        public async Task<Rol> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Rol>()
           .WithOperation(SqlReadOperation.SelectById)
           .WithId(Id)
           .BuildReader();
            Rol rol = new Rol();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                rol = new Rol
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_ROLES")),
                    DescripcionRol = reader.GetString(reader.GetOrdinal("DESCRIPCION_ROLES")),
                };
            }
            reader.Close();
            return rol;
        }

        private Rol MapEntityFromDataRow(DataRow row)
        {
            return new Rol()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_ROLES"),
                DescripcionRol = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_ROLES")
            };
        }
    }
}
