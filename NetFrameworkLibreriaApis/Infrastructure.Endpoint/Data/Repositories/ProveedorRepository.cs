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
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public ProveedorRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(Proveedor proveedor)
        {
            string insertQuery = "INSERT INTO PROVEEDOR (ID_PROVEEDOR,DIRECCION, TELEFONO, CORREO_ELECTRONICO, DESCRIPCION_PROVEEDOR) VALUES(@ID, @Direccion, @Telefono, @CorreoElectronico, @Descripcion)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = proveedor.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Direccion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = proveedor.Direccion
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Telefono",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = proveedor.Telefono
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@CorreoElectronico",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = proveedor.CorreoElectronico
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = proveedor.Descripcion
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM PROVEEDOR WHERE ID_PROVEEDOR = @ProveedorId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@ProveedorId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Proveedor>> Get()
        {
            string query = "SELECT * FROM PROVEEDOR;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<Proveedor> proveedor = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return proveedor;
        }

        public void ModificarProveedor(Guid Id, ProveedorDTO modificarProveedor)
        {
            string updateQuery = "UPDATE PROVEEDOR SET DIRECCION = @Direccion, TELEFONO = @Telefono, CORREO_ELECTRONICO=@CorreoElectronico, DESCRIPCION_PROVEEDOR=@Descripcion WHERE ID_PROVEEDOR = @Id;";
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
                    ParameterName = "@Direccion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarProveedor.Direccion
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Telefono",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarProveedor.Telefono
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@CorreoElectronico",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarProveedor.CorreoElectronico
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarProveedor.Descripcion
                },
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public Proveedor GetById(Guid Id)
        {
            Proveedor proveedor = null;
            string getQuery = "SELECT * FROM PROVEEDOR WHERE ID_PROVEEDOR = @ProveedorId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@ProveedorId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                proveedor = new Proveedor
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_PROVEEDOR")),
                    Direccion = reader.GetString(reader.GetOrdinal("DIRECCION")),
                    Descripcion= reader.GetString(reader.GetOrdinal("DESCRIPCION_PROVEEDOR")),
                    CorreoElectronico = reader.GetString(reader.GetOrdinal("CORREO_ELECTRONICO")),
                    Telefono = reader.GetString(reader.GetOrdinal("TELEFONO")),
                };
            }
            reader.Close();
            return proveedor;

        }

        private Proveedor MapEntityFromDataRow(DataRow row)
        {
            return new Proveedor()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_PROVEEDOR"),
                Direccion = _connectionBuilder.GetDataRowValue<string>(row, "DIRECCION"),
                Descripcion = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_PROVEEDOR"),
                CorreoElectronico = _connectionBuilder.GetDataRowValue<string>(row, "CORREO_ELECTRONICO"),
                Telefono = _connectionBuilder.GetDataRowValue<string>(row, "TELEFONO")
            };
        }
    }
}
