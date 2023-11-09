using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Endpoint.Dtos;
using Infrastructure.Endpoint.Interfaces;
using static Infrastructure.Endpoint.Builders.SqlOperations;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;  

        public EmpleadoRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(Empleado empleado)
        {
            SqlCommand writeCommand = _operationBuilder.From(empleado)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(Empleado empleado)
        {
            SqlCommand writeCommand = _operationBuilder.From(empleado)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<Empleado>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Empleado>()
               .WithOperation(SqlReadOperation.Select)
               .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Empleado> empleados = dt.AsEnumerable().Select(row =>
            new Empleado
            {
                Id = row.Field<Guid>("ID_EMPLEADO"),
                Nombres = row.Field<string>("NOMBRES"),
                Apellidos=row.Field<string>("APELLIDOS"),
                Cedula = row.Field<string>("CEDULA"),
                Telefono = row.Field<string>("TELEFONO")
            }).ToList();

            return empleados;
        }

        public async Task ModificarEmpleado(Empleado modificarEmpleado)
        {
            SqlCommand writeCommand = _operationBuilder.From(modificarEmpleado)
               .WithOperation(SqlWriteOperation.Update)
               .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<Empleado> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Empleado>()
            .WithOperation(SqlReadOperation.SelectById)
            .WithId(Id)
            .BuildReader();
            Empleado empleado = new Empleado();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                empleado = new Empleado
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_EMPLEADO")),
                    Nombres = reader.GetString(reader.GetOrdinal("NOMBRES")),
                    Apellidos=reader.GetString(reader.GetOrdinal("APELLIDOS")),
                    Cedula = reader.GetString(reader.GetOrdinal("CEDULA")),
                    Telefono = reader.GetString(reader.GetOrdinal("TELEFONO"))
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
