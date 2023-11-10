using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Endpoint.Interfaces;
using static Infrastructure.Endpoint.Builders.SqlOperations;
using System.Text.RegularExpressions;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class MaterialRepository : IMaterialRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public MaterialRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(Material material)
        {
            SqlCommand writeCommand = _operationBuilder.From(material)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(Material material)
        {
            SqlCommand writeCommand = _operationBuilder.From(material)
                 .WithOperation(SqlWriteOperation.Delete)
                 .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<Material>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Material>()
                .WithOperation(SqlReadOperation.Select)
                .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Material> material = dt.AsEnumerable().Select(row =>
            new Material
            {
                Id = row.Field<Guid>("ID_MATERIAL"),
                DescripcionMaterial = row.Field<string>("DESCRIPCION_MATERIAL"),
            }).ToList();

            return material;
        }

        public async Task<Material> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Material>()
           .WithOperation(SqlReadOperation.SelectById)
           .WithId(Id)
           .BuildReader();
            Material material = new Material();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                material = new Material
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_MATERIAL")),
                    DescripcionMaterial = reader.GetString(reader.GetOrdinal("DESCRIPCION_MATERIAL")),
                };
            }
            reader.Close();
            return material;
        }

        private Material MapEntityFromDataRow(DataRow row)
        {
            return new Material()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_MATERIAL"),
                DescripcionMaterial = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_MATERIAL")
            };
        }
    }
}
