using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Builders;
using Infrastructure.Endpoint.Interfaces;
using System.Collections.Generic;
using System;
using System.Data;

namespace Infrastructure.Endpoint.Services
{
    public class EntitiesService : IEntitiesService
    {
        private Dictionary<Type, SqlEntitySettings> entities = new Dictionary<Type, SqlEntitySettings>();

        public EntitiesService()
        {
            BuildEntities();
        }

        public SqlEntitySettings GetSettings<TEntity>() where TEntity : BaseEntity
        {
            if (!entities.ContainsKey(typeof(TEntity))) throw new ArgumentOutOfRangeException(nameof(TEntity), "Entidad no encontrada");

            return entities[typeof(TEntity)];
        }

        private void BuildEntities()
        {
            SqlEntitySettings categoriaSettings = GetCategoriaSettings();
            SqlEntitySettings clientesSettings = GetClienteSettings();
            entities.Add(typeof(Categoria), categoriaSettings);
            entities.Add(typeof(Cliente), clientesSettings);
        }

        private SqlEntitySettings GetCategoriaSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_CATEGORIA", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "DESCRIPCION_CATEGORIA", DomainName = "Descripcion", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "CATEGORIA",
                Columns = columns
            };
        }

        private SqlEntitySettings GetClienteSettings()
        {
            var columns = new List<SqlColumnSettings>()
            {
                new SqlColumnSettings() { Name = "ID_CLIENTE", DomainName = "Id", IsPrimaryKey = true, SqlDbType = SqlDbType.UniqueIdentifier },
                new SqlColumnSettings() { Name = "NOMBRES", DomainName = "Nombres", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "CEDULA", DomainName = "Cedula", SqlDbType = SqlDbType.NVarChar },
                new SqlColumnSettings() { Name = "TELEFONO", DomainName = "Telefono", SqlDbType = SqlDbType.NVarChar }
            };

            return new SqlEntitySettings()
            {
                TableName = "CLIENTE",
                Columns = columns
            };
        }
    }
}
