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
    class EmpleadoRepository : IEmpleadoRepository
    {

        private readonly ISingletonSqlConnection _connectionBuilder;  

        public EmpleadoRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(Empleado empleado)
        {
            string insertQuery = "INSERT INTO EMPLEADO (ID_EMPLEADO,NOMBRES,APELLIDOS, CEDULA, TELEFONO) VALUES(@ID, @Nombres,@Apellidos, @Cedula, @Telefono)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = empleado.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Nombres",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = empleado.Nombres
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Apellidos",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = empleado.Apellidos
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Cedula",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = empleado.Cedula
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Telefono", 
                    SqlDbType = SqlDbType.NVarChar,
                    Value = empleado.Telefono
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM EMPLEADO WHERE ID_EMPLEADO = @EmpleadoId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@EmpleadoId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Empleado>> Get()
        {
            string query = "SELECT * FROM EMPLEADO;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<Empleado> empleado = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return empleado;
        }

        public void ModificarEmpleado(Guid Id, EmpleadoDTO modificarEmpleado)
        {
            string updateQuery = "UPDATE EMPLEADO SET NOMBRES = @Nombres,APELLIDOS=@Apellidos, CEDULA = @Cedula, TELEFONO=@Telefono WHERE ID_EMPLEADO = @Id;";
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
                    Value = modificarEmpleado.Nombres
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Apellidos",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarEmpleado.Apellidos
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Cedula",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarEmpleado.Cedula
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Telefono",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarEmpleado.Telefono
                },
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public Empleado GetById(Guid Id)
        {
            Empleado empleado = null;
            string getQuery = "SELECT * FROM EMPLEADO WHERE ID_EMPLEADO = @EmpleadoId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@EmpleadoId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                empleado = new Empleado
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_EMPLEADO")),
                    Nombres = reader.GetString(reader.GetOrdinal("NOMBRES")),
                    Apellidos = reader.GetString(reader.GetOrdinal("APELLIDOS")),
                    Cedula = reader.GetString(reader.GetOrdinal("CEDULA")),
                    Telefono = reader.GetString(reader.GetOrdinal("TELEFONO")),
                };
            }
            reader.Close();
            return empleado;

        }

        private Empleado MapEntityFromDataRow(DataRow row)
        {
            return new Empleado()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_EMPLEADO"),
                Nombres = _connectionBuilder.GetDataRowValue<string>(row, "NOMBRES"),
                Apellidos = _connectionBuilder.GetDataRowValue<string>(row, "APELLIDOS"),
                Cedula = _connectionBuilder.GetDataRowValue<string>(row, "CEDULA"),
                Telefono = _connectionBuilder.GetDataRowValue<string>(row, "TELEFONO")
            };
        }
    }
}
