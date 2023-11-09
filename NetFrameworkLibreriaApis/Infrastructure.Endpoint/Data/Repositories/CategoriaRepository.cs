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

            //string query = "SELECT * FROM CATEGORIA;";
            //DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            //List<Categoria> cat = dataTable.AsEnumerable()
            //    .Select(MapEntityFromDataRow)
            //    .ToList();

            //return cat;
        }

        public void Create(Categoria categoria)
        {
            SqlCommand writeCommand = _operationBuilder.From(categoria)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);

            //string insertQuery = "INSERT INTO CATEGORIA (ID_CATEGORIA,DESCRIPCION_CATEGORIA) VALUES(@ID, @Descripcion)";
            //SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            //SqlParameter[] parameters = new SqlParameter[]
            //{
            //    new SqlParameter() {
            //        Direction = ParameterDirection.Input,
            //        ParameterName = "@ID",
            //        SqlDbType = SqlDbType.UniqueIdentifier,
            //        Value = categoria.Id
            //    },
            //    new SqlParameter() {
            //        Direction = ParameterDirection.Input,
            //        ParameterName = "@Descripcion",
            //        SqlDbType = SqlDbType.NVarChar,
            //        Value = categoria.Descripcion
            //    }
            //};
            //sqlCommand.Parameters.AddRange(parameters);
            //sqlCommand.ExecuteNonQuery();
        }

        public async Task Eliminar(Categoria categoria)
        {
            SqlCommand writeCommand = _operationBuilder.From(categoria)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);

            //string deleteQuery = "DELETE FROM CATEGORIA WHERE ID_CATEGORIA = @CategoriaId;";
            //SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            //SqlParameter parameter = new SqlParameter()
            //{
            //    Direction = ParameterDirection.Input,
            //    ParameterName = "@CategoriaId",
            //    SqlDbType = SqlDbType.UniqueIdentifier,
            //    Value = Id
            //};
            //sqlCommand.Parameters.Add(parameter);
            //sqlCommand.ExecuteNonQuery();
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


            //Categoria cat = null;
            //string getQuery = "SELECT * FROM CATEGORIA WHERE ID_CATEGORIA = @CategoriaId;";
            //SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            //SqlParameter parameter = new SqlParameter()
            //{
            //    Direction = ParameterDirection.Input,
            //    ParameterName = "@CategoriaId",
            //    SqlDbType = SqlDbType.UniqueIdentifier,
            //    Value = Id
            //};
            //sqlCommand.Parameters.Add(parameter);
            //SqlDataReader reader = sqlCommand.ExecuteReader();
            //if (reader.Read())
            //{
            //    cat = new Categoria
            //    {
            //        Id = reader.GetGuid(reader.GetOrdinal("ID_CATEGORIA")),
            //        Descripcion = reader.GetString(reader.GetOrdinal("DESCRIPCION_CATEGORIA")),      
            //    };
            //}
            //reader.Close();
            //return cat;
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
