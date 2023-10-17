using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class MedidaRepository : IMedidaRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public MedidaRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(UnidadMedida medida)
        {
            string insertQuery = "INSERT INTO UNIDAD_MEDIDA (ID_UNIDAD_MEDIDA,DESCRIPCION_MEDIDA) VALUES(@ID, @Descripcion)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = medida.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = medida.DescripcionMedida
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM UNIDAD_MEDIDA WHERE ID_UNIDAD_MEDIDA = @MedidaId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@MedidaId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<UnidadMedida>> Get()
        {
            string query = "SELECT * FROM UNIDAD_MEDIDA;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<UnidadMedida> medida = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return medida;
        }

        public UnidadMedida GetById(Guid Id)
        {
            UnidadMedida medida = null;
            string getQuery = "SELECT * FROM UNIDAD_MEDIDA WHERE ID_UNIDAD_MEDIDA = @MedidaId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@MedidaId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                medida = new UnidadMedida
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_UNIDAD_MEDIDA")),
                    DescripcionMedida = reader.GetString(reader.GetOrdinal("DESCRIPCION_MEDIDA")),
                };
            }
            reader.Close();
            return medida;

        }

        private UnidadMedida MapEntityFromDataRow(DataRow row)
        {
            return new UnidadMedida()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_UNIDAD_MEDIDA"),
                DescripcionMedida = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_MEDIDA")
            };
        }
    }
}
