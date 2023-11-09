using Domain.Endpoint.Entities;
using static Infrastructure.Endpoint.Builders.SqlOperations;
using System.Data.SqlClient;
using System;

namespace Infrastructure.Endpoint.Interfaces
{
    public interface ISqlCommandOperationBuilder
    {
        IHaveSqlWriteOperation From<TEntity>(TEntity entity) where TEntity : BaseEntity;
        IHaveSqlReadOperation Initialize<TEntity>() where TEntity : BaseEntity;
    }

    public interface IHaveSqlWriteOperation
    {
        IExecuteWriteBuilder WithOperation(SqlWriteOperation operation);
    }

    public interface IExecuteWriteBuilder
    {
        SqlCommand BuildWritter();
    }

    public interface IHaveSqlReadOperation
    {
        IHavePrimaryKeyValue WithOperation(SqlReadOperation operation);
    }

    public interface IHavePrimaryKeyValue : IExecuteReadBuilder
    {
        IExecuteReadBuilder WithId(Guid id);
    }

    public interface IExecuteReadBuilder
    {
        SqlCommand BuildReader();
    }
}
