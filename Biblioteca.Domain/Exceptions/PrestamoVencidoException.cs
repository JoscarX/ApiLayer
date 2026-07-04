namespace Biblioteca.Domain.Exceptions;

/// <summary>Se lanza cuando se opera sobre un préstamo que ya está vencido.</summary>
public sealed class PrestamoVencidoException : DomainException
{
    public PrestamoVencidoException(int prestamoId)
        : base($"El préstamo #{prestamoId} está vencido y no puede ser modificado.") { }
}
