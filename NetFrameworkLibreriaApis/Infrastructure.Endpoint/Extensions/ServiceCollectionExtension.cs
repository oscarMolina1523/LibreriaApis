using Domain.Endpoint.Interfaces.Repositories;
using Infrastructure.Endpoint.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Endpoint.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IMedidaRepository, MedidaRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IDetalleProductoRepository, DetalleProductoRepository>();
            services.AddSingleton<ISingletonSqlConnection>(SingletonSqlConnection.GetInstance());
            return services;
        }
    }
}
