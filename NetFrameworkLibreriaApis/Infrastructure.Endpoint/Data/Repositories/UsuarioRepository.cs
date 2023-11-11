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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public UsuarioRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(Usuario usuario)
        {
            SqlCommand writeCommand = _operationBuilder.From(usuario)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(Usuario usuario)
        {
            SqlCommand writeCommand = _operationBuilder.From(usuario)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<Usuario>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Usuario>()
                .WithOperation(SqlReadOperation.Select)
                .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<Usuario> usuarios = dt.AsEnumerable().Select(row =>
            new Usuario
            {
                Id = row.Field<Guid>("ID_USUARIO"),
                IdEmpleado = row.Field<Guid>("ID_EMPLEADO"),
                IdRol = row.Field<Guid>("ID_ROLES"),
                NombreUsuario = row.Field<string>("NOMBRE_USUARIO"),
                Contraseña = row.Field<string>("CONTRASEÑA"),
            }).ToList();

            return usuarios;
        }

        public async Task ModificarUsuario(Usuario modificarUsuario)
        {
            SqlCommand writeCommand = _operationBuilder.From(modificarUsuario)
               .WithOperation(SqlWriteOperation.Update)
               .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<Usuario> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<Usuario>()
            .WithOperation(SqlReadOperation.SelectById)
            .WithId(Id)
            .BuildReader();
            Usuario usuario = new Usuario();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
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
