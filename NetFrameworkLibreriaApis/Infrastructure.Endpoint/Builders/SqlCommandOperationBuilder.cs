using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Interfaces;
using Infrastructure.Endpoint.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using static Infrastructure.Endpoint.Builders.SqlOperations;

namespace Infrastructure.Endpoint.Builders
{
    public class SqlCommandOperationBuilder : ISqlCommandOperationBuilder
    {
        private readonly IEntitiesService _entitiesService;

        public SqlCommandOperationBuilder(IEntitiesService entitiesService)
        {
            _entitiesService=entitiesService;
        }

        public IHaveSqlWriteOperation From<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return new SqlCommandOperationBuilder<TEntity>(entity, _entitiesService);
        }

        public IHaveSqlReadOperation Initialize<TEntity>() where TEntity : BaseEntity
        {
            return new SqlCommandOperationBuilder<TEntity>(_entitiesService);
        }
    }

    public class SqlCommandOperationBuilder<TEntity> :
        IHaveSqlWriteOperation,
        IExecuteWriteBuilder,
        IHaveSqlReadOperation,
        IHavePrimaryKeyValue,
        IExecuteReadBuilder
        where TEntity : BaseEntity
    {
        private readonly TEntity entity;
        private SqlWriteOperation writeOperation;
        private SqlReadOperation readOperation;
        private readonly IEntitiesService entityService;
        private Guid id;

        public SqlCommandOperationBuilder(TEntity entity, IEntitiesService entityService)
        {
            this.entity = entity;
            this.entityService = entityService;
        }

        public SqlCommandOperationBuilder(IEntitiesService entityService)
        {
            this.entityService = entityService;
        }

        public IExecuteWriteBuilder WithOperation(SqlWriteOperation operation)
        {
            writeOperation = operation;
            return this;
        }

        public IExecuteReadBuilder WithId(Guid id)
        {
            this.id = id;
            return this;
        }

        public IHavePrimaryKeyValue WithOperation(SqlReadOperation operation)
        {
            readOperation = operation;
            return this;
        }

        //aca este es el comando de creacion de un nuevo registro

        private SqlCommand GetInsertCommand()
        {
            SqlEntitySettings entitySettings = entityService.GetSettings<TEntity>();
            string sqlQuery = GetInsertQuery(entitySettings.NormalizedTableName, entitySettings.Columns);
            List<SqlParameter> parameters = GetSqlParameters(entity, entitySettings.Columns);
            SqlCommand command = new SqlCommand(sqlQuery);
            command.Parameters.AddRange(parameters.ToArray());
            return command;
        }

        private string GetInsertQuery(string entityName, List<SqlColumnSettings> columnSettings)
        {
            StringBuilder builder = new StringBuilder();
            List<string> columns = columnSettings.Where(column => !column.IsComputedColumn)
                .Select(column => column.Name)
                .ToList();
            List<string> parameters = columnSettings.Where(column => !column.IsComputedColumn)
                .Select(setting => setting.ParameterName)
                .ToList();
            builder.Append("INSERT INTO ")
                .Append($"{entityName} (")
                .Append(string.Join(",", columns))
                .Append(") ")
                .Append("VALUES (")
                .Append(string.Join(",", parameters))
                .Append(");");

            return builder.ToString();
        }

        private List<SqlParameter> GetSqlParameters(TEntity entitty, List<SqlColumnSettings> columns)
        {
            Type entityType = entitty.GetType();
            List<PropertyInfo> properties = entityType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToList();
            List<SqlParameter> parameters = new List<SqlParameter>();

            foreach (PropertyInfo property in properties)
            {
                //aca en esta comparacion no te olvides que el Property.Name no es el de la entidad sino
                //uno del property no son lo mismo, estas equivocado si pensas que son lo mismo
                SqlColumnSettings column = columns.Where(c => c.DomainName == property.Name).FirstOrDefault();

                if (column is null) continue;
                if (column.IsComputedColumn) continue;

                SqlParameter sqlParameter = new SqlParameter()
                {
                    SqlDbType = column.SqlDbType,
                    Direction = ParameterDirection.Input,
                    ParameterName = column.ParameterName,
                    Value = GetDefaultValue(entity, property, column),
                    IsNullable = column.IsNullable
                };

                parameters.Add(sqlParameter);
            }

            return parameters;
        }

        private object GetDefaultValue(TEntity entity, PropertyInfo property, SqlColumnSettings column)
        {
            object value = property.GetValue(entity);
            if (value is null)
            {
                return column.IsNullable ? DBNull.Value : Activator.CreateInstance(property.PropertyType);
            }

            return value;
        }

        private SqlCommand GetUpdateCommand()
        {
            SqlEntitySettings entitySettings = entityService.GetSettings<TEntity>();
            string sqlQuery = GetUpdateQuery(entitySettings.NormalizedTableName, entitySettings.Columns);
            List<SqlParameter> parameters = GetSqlParameters(entity, entitySettings.Columns);
            SqlCommand command = new SqlCommand(sqlQuery);
            command.Parameters.AddRange(parameters.ToArray());
            return command;
        }

        private string GetUpdateQuery(string entityName, List<SqlColumnSettings> columnSettings)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"UPDATE {entityName} SET ");

            //aqui creo que esta calculando la ultima columna de la lista, pero no se, no creo estar equivocado
            int lastIndex = columnSettings.Count - 1;

            SqlColumnSettings primaryKey = columnSettings.Where(column => column.IsPrimaryKey).FirstOrDefault();
            if (primaryKey is null) throw new Exception("No Primary Key Found");

            foreach (var data in columnSettings.Select((columnSetting, index) => (columnSetting, index)))
            {
                SqlColumnSettings columnSetting = data.columnSetting;
                if (columnSetting.IsPrimaryKey) continue;
                if (columnSetting.IsComputedColumn) continue;

                builder.Append($"{columnSetting.Name} = {columnSetting.ParameterName}");

                //aca lo esta usando para saber si es la ultima no debe de agregar coma
                builder.Append(lastIndex.Equals(data.index) ? " " : ",");
            }

            builder.Append($"WHERE {primaryKey.Name} = {primaryKey.ParameterName};");
            return builder.ToString();
        }

        private SqlCommand GetDeleteCommand()
        {
            SqlEntitySettings entitySettings = entityService.GetSettings<TEntity>();
            string sqlQuery = GetDeleteQuery(entitySettings.NormalizedTableName, entitySettings.Columns);
            List<SqlParameter> parameters = GetSqlParameters(entity, entitySettings.Columns);
            SqlCommand command = new SqlCommand(sqlQuery);
            command.Parameters.AddRange(parameters.ToArray());
            return command;
        }

        private string GetDeleteQuery(string entityName, List<SqlColumnSettings> columnSettings)
        {
            StringBuilder builder = new StringBuilder();
            SqlColumnSettings primaryKey = columnSettings.Where(column => column.IsPrimaryKey).FirstOrDefault();
            if (primaryKey is null) throw new Exception("No Primary Key Found");

            return builder.Append("DELETE FROM ")
                .Append(entityName)
                .Append(" WHERE ")
                .Append(primaryKey.Name)
                .Append(" = ")
                .Append(primaryKey.ParameterName)
                .Append(";")
                .ToString();
        }

        public SqlCommand BuildWritter()
        {
            switch (writeOperation)
            {
                case SqlWriteOperation.Create: return GetInsertCommand();
                case SqlWriteOperation.Update: return GetUpdateCommand();
                case SqlWriteOperation.Delete: return GetDeleteCommand();

                default: throw new ArgumentOutOfRangeException(nameof(writeOperation), "Invalid Operation!");
            }
        }

        public SqlCommand BuildReader()
        {
            switch (readOperation)
            {
                case SqlReadOperation.Select: return GetSelectCommand();
                case SqlReadOperation.SelectById: return GetSelectByIdCommand();

                default: throw new ArgumentOutOfRangeException(nameof(writeOperation), "Invalid Operation!");
            }
        }

        private SqlCommand GetSelectCommand()
        {
            SqlEntitySettings entitySettings = entityService.GetSettings<TEntity>();
            string tableName = entitySettings.NormalizedTableName;
            string sqlQuery = $"SELECT * FROM {tableName};";
            SqlCommand command = new SqlCommand(sqlQuery);
            command.CommandType = CommandType.Text;
            return command;
        }

        private SqlCommand GetSelectByIdCommand()
        {
            SqlEntitySettings entitySettings = entityService.GetSettings<TEntity>();
            SqlColumnSettings primaryKey = entitySettings.Columns.Where(column => column.IsPrimaryKey).FirstOrDefault();
            if (primaryKey is null) throw new Exception("No Primary Key Found");

            string tableName = entitySettings.NormalizedTableName;
            string sqlQuery = $"SELECT * FROM {tableName} WHERE {primaryKey.Name} = {primaryKey.ParameterName};";
            SqlCommand command = new SqlCommand(sqlQuery);
            command.CommandType = CommandType.Text;

            SqlParameter sqlParameter = new SqlParameter()
            {
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input,
                ParameterName = primaryKey.ParameterName,
                Value = id,
            };

            command.Parameters.Add(sqlParameter);
            return command;
        }

    }
}
