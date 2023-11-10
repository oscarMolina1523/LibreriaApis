using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Endpoint.Interfaces;
using static Infrastructure.Endpoint.Builders.SqlOperations;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class MedidaRepository : IMedidaRepository
    {
        private readonly ISqlCommandOperationBuilder _operationBuilder;
        private readonly ISingletonSqlConnection _connectionBuilder;

        public MedidaRepository(ISingletonSqlConnection connectionBuilder, ISqlCommandOperationBuilder operationBuilder)
        {
            _connectionBuilder = connectionBuilder;
            _operationBuilder = operationBuilder;
        }

        public void Create(UnidadMedida medida)
        {
            SqlCommand writeCommand = _operationBuilder.From(medida)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task Eliminar(UnidadMedida medida)
        {
            SqlCommand writeCommand = _operationBuilder.From(medida)
                  .WithOperation(SqlWriteOperation.Delete)
                  .BuildWritter();
            await _connectionBuilder.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public async Task<List<UnidadMedida>> Get()
        {
            SqlCommand readCommand = _operationBuilder.Initialize<UnidadMedida>()
                .WithOperation(SqlReadOperation.Select)
                .BuildReader();
            DataTable dt = await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);

            List<UnidadMedida> medida = dt.AsEnumerable().Select(row =>
            new UnidadMedida
            {
                Id = row.Field<Guid>("ID_UNIDAD_MEDIDA"),
                DescripcionMedida = row.Field<string>("DESCRIPCION_MEDIDA"),
            }).ToList();

            return medida;
        }

        public async Task<UnidadMedida> GetById(Guid Id)
        {
            SqlCommand readCommand = _operationBuilder.Initialize<UnidadMedida>()
           .WithOperation(SqlReadOperation.SelectById)
           .WithId(Id)
           .BuildReader();
            UnidadMedida medida = new UnidadMedida();
            await _connectionBuilder.ExecuteQueryCommandAsync(readCommand);
            SqlDataReader reader = readCommand.ExecuteReader();
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
