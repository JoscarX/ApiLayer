namespace Biblioteca.Domain.Exceptions;

/// <summary>Se lanza cuando se intenta prestar un libro sin disponibilidad.</summary>
public sealed class LibroNoDisponibleException : DomainException
{
    public LibroNoDisponibleException(string titulo)
        : base($"El libro '{titulo}' no está disponible para préstamo.") { }
}
