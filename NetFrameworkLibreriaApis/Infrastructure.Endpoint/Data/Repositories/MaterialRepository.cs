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
    class MaterialRepository : IMaterialRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public MaterialRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(Material material)
        {
            string insertQuery = "INSERT INTO MATERIAL (ID_MATERIAL,DESCRIPCION_MATERIAL) VALUES(@ID, @Descripcion)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = material.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = material.DescripcionMaterial
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM MATERIAL WHERE ID_MATERIAL = @MaterialId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@MaterialId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Material>> Get()
        {
            string query = "SELECT * FROM MATERIAL;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<Material> material = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return material;
        }

        public Material GetById(Guid Id)
        {
            Material material = null;
            string getQuery = "SELECT * FROM MATERIAL WHERE ID_MATERIAL = @MaterialId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@MaterialId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
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
