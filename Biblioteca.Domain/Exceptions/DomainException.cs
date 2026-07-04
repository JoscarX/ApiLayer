namespace Biblioteca.Domain.Exceptions;

/// <summary>
/// Excepción base del dominio. Todas las excepciones de negocio heredan de esta.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string mensaje) : base(mensaje) { }

    public DomainException(string mensaje, Exception innerException)
        : base(mensaje, innerException) { }
}
