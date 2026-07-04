using Biblioteca.Application.Common.Interfaces;
using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Biblioteca.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

        try
        {

            if (!await context.Roles.AnyAsync())
            {
                logger.LogInformation("Seeding Roles...");
                var rolAdmin = Rol.Crear("Administrador", "Acceso total al sistema");
                var rolBiblio = Rol.Crear("Bibliotecario", "Gestión de catálogo y préstamos");

                await context.Roles.AddRangeAsync(rolAdmin, rolBiblio);
                await context.SaveChangesAsync();

                logger.LogInformation("Seeding Admin User...");
                var hash = passwordHasher.Hash("Admin123.");
                var admin = Usuario.Crear("Super Admin", "0000000000", hash, rolAdmin.Id);

                await context.Usuarios.AddAsync(admin);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while seeding the database.");
            throw;
        }
    }
}
