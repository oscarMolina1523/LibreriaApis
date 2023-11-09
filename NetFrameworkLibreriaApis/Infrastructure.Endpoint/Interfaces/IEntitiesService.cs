using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Builders;

namespace Infrastructure.Endpoint.Interfaces
{
    public interface IEntitiesService
    {
        SqlEntitySettings GetSettings<TEntity>() where TEntity : BaseEntity;
    }
}
