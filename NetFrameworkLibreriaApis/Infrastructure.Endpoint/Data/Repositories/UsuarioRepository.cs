using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Endpoint.Dtos;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public UsuarioRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(Usuario usuario)
        {
            string insertQuery = "INSERT INTO USUARIO (ID_USUARIO,ID_ROLES, ID_EMPLEADO,NOMBRE_USUARIO, CONTRASEÑA) VALUES(@ID, @IdRoles, @IdEmpleado,@NombreUsuario,  @Contraseña)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = usuario.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdRoles",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = usuario.IdRol
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdEmpleado",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = usuario.IdEmpleado
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@NombreUsuario",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = usuario.NombreUsuario
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Contraseña",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = usuario.Contraseña
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM USUARIO WHERE ID_USUARIO = @UsuarioId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@UsuarioId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Usuario>> Get()
        {
            string query = "SELECT * FROM USUARIO;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<Usuario> usuario = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return usuario;
        }

        public void ModificarUsuario(Guid Id, UsuarioDTO modificarUsuario)
        {
            string updateQuery = "UPDATE USUARIO SET ID_EMPLEADO = @IdEmpleado, ID_ROLES = @IdRol, NOMBRE_USUARIO=@NombreUsuario, CONTRASEÑA=@Contraseña WHERE ID_USUARIO = @Id;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(updateQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdEmpleado",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = modificarUsuario.IdEmpleado
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdRol",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = modificarUsuario.IdRol
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@NombreUsuario",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarUsuario.NombreUsuario
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Contraseña",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarUsuario.Contraseña
                },
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public Usuario GetById(Guid Id)
        {
            Usuario usuario = null;
            string getQuery = "SELECT * FROM USUARIO WHERE ID_USUARIO = @UsuarioId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@UsuarioId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                usuario = new Usuario
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_USUARIO")),
                    IdEmpleado = reader.GetGuid(reader.GetOrdinal("ID_EMPLEADO")),
                    IdRol = reader.GetGuid(reader.GetOrdinal("ID_ROLES")),
                    NombreUsuario = reader.GetString(reader.GetOrdinal("NOMBRE_USUARIO")),
                    Contraseña = reader.GetString(reader.GetOrdinal("CONTRASEÑA")),
                };
            }
            reader.Close();
            return usuario;
        }

        private Usuario MapEntityFromDataRow(DataRow row)
        {
            return new Usuario()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_USUARIO"),
                IdEmpleado = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_EMPLEADO"),
                IdRol = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_ROLES"),
                NombreUsuario = _connectionBuilder.GetDataRowValue<string>(row, "NOMBRE_USUARIO"),
                Contraseña = _connectionBuilder.GetDataRowValue<string>(row, "CONTRASEÑA")
            };
        }
    }
}
