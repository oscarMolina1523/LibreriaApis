using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class ClienteRepository : IClienteRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public ClienteRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(Cliente cliente)
        {
            string insertQuery = "INSERT INTO CLIENTE (ID_CLIENTE,NOMBRES, CEDULA, TELEFONO) VALUES(@ID, @Nombres, @Cedula, @Telefono)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = cliente.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Nombres",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = cliente.Nombres
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Cedula",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = cliente.Cedula
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Telefono",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = cliente.Telefono
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM CLIENTE WHERE ID_CLIENTE = @ClienteId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@ClienteId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Cliente>> Get()
        {
            string query = "SELECT * FROM CLIENTE;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<Cliente> cliente = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return cliente;
        }

        public void ModificarCliente(Guid Id, ClienteDTO modificarCliente)
        {
            string updateQuery = "UPDATE CLIENTE SET NOMBRES = @Nombres, CEDULA = @Cedula, TELEFONO=@Telefono WHERE ID_CLIENTE = @Id;";
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
                    ParameterName = "@Nombres",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarCliente.Nombres
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Cedula",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarCliente.Cedula
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Telefono",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarCliente.Telefono
                },
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public Cliente GetById(Guid Id)
        {
            Cliente cliente = null;
            string getQuery = "SELECT * FROM CLIENTE WHERE ID_CLIENTE = @ClienteId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@ClienteId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                cliente = new Cliente
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_CLIENTE")),
                    Nombres = reader.GetString(reader.GetOrdinal("NOMBRES")),
                    Cedula = reader.GetString(reader.GetOrdinal("CEDULA")),
                    Telefono = reader.GetString(reader.GetOrdinal("TELEFONO")),
                };
            }
            reader.Close();
            return cliente;

        }

        private Cliente MapEntityFromDataRow(DataRow row)
        {
            return new Cliente()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_CLIENTE"),
                Nombres= _connectionBuilder.GetDataRowValue<string>(row, "NOMBRES"),
                Cedula= _connectionBuilder.GetDataRowValue<string>(row, "CEDULA"),
                Telefono = _connectionBuilder.GetDataRowValue<string>(row, "TELEFONO")
            };
        }
    }
}
