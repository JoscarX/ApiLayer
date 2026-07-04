using Biblioteca.Application.Common.Interfaces;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Biblioteca.Infrastructure.Data.Interceptors;
using Biblioteca.Infrastructure.Data.Repositories;
using Biblioteca.Infrastructure.Identity;
using Biblioteca.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Interceptors
        services.AddScoped<AuditableEntityInterceptor>();

        // DbContext
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        // Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<IVisitanteRepository, VisitanteRepository>();
        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<IEditorialRepository, EditorialRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ILibroRepository, LibroRepository>();
        services.AddScoped<IPrestamoRepository, PrestamoRepository>();
        services.AddScoped<IEntradaRepository, EntradaRepository>();
        services.AddScoped<IVentaRepository, VentaRepository>();
        services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Identity & Services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        // Options
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        return services;
    }
}
