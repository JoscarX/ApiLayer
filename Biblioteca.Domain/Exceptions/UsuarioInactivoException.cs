namespace Biblioteca.Domain.Exceptions;

/// <summary>Se lanza cuando se opera con un usuario inactivo.</summary>
public sealed class UsuarioInactivoException : DomainException
{
    public UsuarioInactivoException(string nombre)
        : base($"El usuario '{nombre}' está inactivo y no puede realizar esta operación.") { }
}
