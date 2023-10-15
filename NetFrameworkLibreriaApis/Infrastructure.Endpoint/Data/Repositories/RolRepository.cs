using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class RolRepository : IRolRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public RolRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(Rol rol)
        {
            string insertQuery = "INSERT INTO ROLES (ID_ROLES,DESCRIPCION_ROLES) VALUES(@ID, @Descripcion)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = rol.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = rol.DescripcionRol
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM ROLES WHERE ID_ROLES = @RolesId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@RolesId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Rol>> Get()
        {
            string query = "SELECT * FROM ROLES;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<Rol> rol = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return rol;
        }

        public Rol GetById(Guid Id)
        {
            Rol rol = null;
            string getQuery = "SELECT * FROM ROLES WHERE ID_ROLES = @RolesId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@RolesId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
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
