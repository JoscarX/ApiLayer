using Biblioteca.Application.Common.Interfaces;

namespace Biblioteca.Infrastructure.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime AhoraUtc => DateTime.UtcNow;
    
    // Suponemos zona horaria local del servidor, pero idealmente se inyectaría según configuración o usuario
    public DateTime Ahora => DateTime.Now;
    
    public DateOnly HoyUtc => DateOnly.FromDateTime(DateTime.UtcNow);
}
