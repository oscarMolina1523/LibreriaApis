using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Endpoint.Interfaces;
using static Infrastructure.Endpoint.Builders.SqlOperations;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class MarcaRepository : IMarcaRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public MarcaRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(Marca marca)
        {
            SqlCommand writeCommand = _operationBuilder.From(marca)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(Marca marca)
        {
            SqlCommand writeCommand = _operationBuilder.From(marca)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<Marca>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Marca>()
                .WithOperation(SqlReadOperation.Select)
                .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Marca> marcas = dt.AsEnumerable().Select(row =>
            new Marca
            {
                Id = row.Field<Guid>("ID_MARCA"),
                DescripcionMarca = row.Field<string>("DESCRIPCION_MARCA"),
            }).ToList();

            return marcas;
        }

        public async Task<Marca> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Marca>()
            .WithOperation(SqlReadOperation.SelectById)
            .WithId(Id)
            .BuildReader();
            Marca marca = new Marca();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                marca = new Marca
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_MARCA")),
                    DescripcionMarca = reader.GetString(reader.GetOrdinal("DESCRIPCION_MARCA")),
                };
            }
            reader.Close();
            return marca;

        }

        private Marca MapEntityFromDataRow(DataRow row)
        {
            return new Marca()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_MARCA"),
                DescripcionMarca = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_MARCA")
            };
        }
    }
}
