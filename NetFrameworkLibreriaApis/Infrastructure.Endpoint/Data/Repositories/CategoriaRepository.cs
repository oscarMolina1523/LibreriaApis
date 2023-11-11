using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Infrastructure.Endpoint.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Infrastructure.Endpoint.Builders.SqlOperations;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public CategoriaRepository( ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public async Task<List<Categoria>> Get()
        {

            SqlCommand readCommand = _operationBuilder.Initialize<Categoria>()
                .WithOperation(SqlReadOperation.Select)
                .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Categoria> categorias = dt.AsEnumerable().Select(row =>
            new Categoria
            {
                Id = row.Field<Guid>("ID_CATEGORIA"),
                Descripcion = row.Field<string>("DESCRIPCION_CATEGORIA"),
            }).ToList();

            return categorias;
        }

        public void Create(Categoria categoria)
        {
            SqlCommand writeCommand = _operationBuilder.From(categoria)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);

        }

        public async Task Eliminar(Categoria categoria)
        {
            SqlCommand writeCommand = _operationBuilder.From(categoria)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);

        }

        public async Task<Categoria> GetById(Guid Id)
        {

             SqlCommand readCommand = _operationBuilder.Initialize<Categoria>()
            .WithOperation(SqlReadOperation.SelectById)
            .WithId(Id)
            .BuildReader();
            Categoria cat = new Categoria();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                 cat = new Categoria
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_CATEGORIA")),
                    Descripcion = reader.GetString(reader.GetOrdinal("DESCRIPCION_CATEGORIA")),
                };
            }
            reader.Close();
            return cat;
        }

        private Categoria MapEntityFromDataRow(DataRow row)
        {
            return new Categoria()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_CATEGORIA"),
                Descripcion = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_CATEGORIA")
            };
        }
    }
}
