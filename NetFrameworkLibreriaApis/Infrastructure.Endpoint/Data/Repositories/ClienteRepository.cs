using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Infrastructure.Endpoint.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Infrastructure.Endpoint.Builders.SqlOperations;

namespace Infrastructure.Endpoint.Data.Repositories
{
    class ClienteRepository : IClienteRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public ClienteRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(Cliente cliente)
        {
            SqlCommand writeCommand = _operationBuilder.From(cliente)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
            
        }

        public async Task Eliminar(Cliente cliente)
        {
            SqlCommand writeCommand = _operationBuilder.From(cliente)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
            
        }

        public async Task<List<Cliente>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Cliente>()
               .WithOperation(SqlReadOperation.Select)
               .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Cliente> clientes = dt.AsEnumerable().Select(row =>
            new Cliente
            {
                Id = row.Field<Guid>("ID_CLIENTE"),
                Nombres = row.Field<string>("NOMBRES"),
                Cedula=row.Field<string>("CEDULA"),
                Telefono=row.Field<string>("TELEFONO")
            }).ToList();

            return clientes;
            
        }

        public async Task ModificarCliente(Cliente modificarCliente)
        {
            SqlCommand writeCommand = _operationBuilder.From(modificarCliente)
               .WithOperation(SqlWriteOperation.Update)
               .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);

        }

        public async Task<Cliente> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Cliente>()
            .WithOperation(SqlReadOperation.SelectById)
            .WithId(Id)
            .BuildReader();
            Cliente cliente = new Cliente();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
            if (reader.Read())
            {
                cliente = new Cliente
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_CLIENTE")),
                    Nombres = reader.GetString(reader.GetOrdinal("NOMBRES")),
                    Cedula = reader.GetString(reader.GetOrdinal("CEDULA")),
                    Telefono = reader.GetString(reader.GetOrdinal("TELEFONO"))
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
