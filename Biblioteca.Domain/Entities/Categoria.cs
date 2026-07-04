using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa una categoría literaria (novela, ciencia ficción, etc.).
/// Un libro puede pertenecer a múltiples categorías.
/// </summary>
public sealed class Categoria : AuditableEntity
{
    public string Nombre { get; private set; } = string.Empty;

    // Navegación
    private readonly List<Libro> _libros = [];
    public IReadOnlyCollection<Libro> Libros => _libros.AsReadOnly();

    private Categoria() { }

    /// <summary>Crea una nueva categoría.</summary>
    public static Categoria Crear(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre de la categoría es obligatorio.");

        if (nombre.Length > 50)
            throw new DomainException("El nombre de la categoría no puede exceder 50 caracteres.");

        return new Categoria { Nombre = nombre.Trim() };
    }

    /// <summary>Actualiza el nombre de la categoría.</summary>
    public void Actualizar(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre de la categoría es obligatorio.");

        Nombre = nombre.Trim();
    }
}
