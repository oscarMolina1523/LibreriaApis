using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class MarcaRepository : IMarcaRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public MarcaRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(Marca marca)
        {
            string insertQuery = "INSERT INTO MARCA (ID_MARCA,DESCRIPCION_MARCA) VALUES(@ID, @Descripcion)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = marca.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = marca.DescripcionMarca
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM MARCA WHERE ID_MARCA = @MarcaId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@MarcaId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Marca>> Get()
        {
            string query = "SELECT * FROM MARCA;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<Marca> marca = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return marca;
        }

        public Marca GetById(Guid Id)
        {
            Marca marca = null;
            string getQuery = "SELECT * FROM MARCA WHERE ID_MARCA = @MarcaId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@MarcaId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
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
