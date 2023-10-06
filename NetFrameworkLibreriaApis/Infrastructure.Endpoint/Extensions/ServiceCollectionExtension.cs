using Domain.Endpoint.Interfaces.Repositories;
using Infrastructure.Endpoint.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Endpoint.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ICategoriaRepository, CategoriaRepository>();
            services.AddSingleton<IMarcaRepository, MarcaRepository>();
            services.AddSingleton<IMedidaRepository, MedidaRepository>();
            services.AddSingleton<IMaterialRepository, MaterialRepository>();
            services.AddSingleton<IRolRepository, RolRepository>();
            services.AddSingleton<IEmpleadoRepository, EmpleadoRepository>();
            services.AddSingleton<IClienteRepository, ClienteRepository>();
            services.AddSingleton<IProveedorRepository, ProveedorRepository>();
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IProductoRepository, ProductoRepository>();
            services.AddSingleton<IDetalleProductoRepository, DetalleProductoRepository>();
            return services;
        }
    }
}
