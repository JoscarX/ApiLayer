using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa una editorial que publica libros.
/// </summary>
public sealed class Editorial : AuditableEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public string? Pais { get; private set; }

    // Navegación
    private readonly List<Libro> _libros = [];
    public IReadOnlyCollection<Libro> Libros => _libros.AsReadOnly();

    private Editorial() { }

    /// <summary>Crea una nueva editorial.</summary>
    public static Editorial Crear(string nombre, string? pais = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre de la editorial es obligatorio.");

        if (nombre.Length > 120)
            throw new DomainException("El nombre de la editorial no puede exceder 120 caracteres.");

        return new Editorial
        {
            Nombre = nombre.Trim(),
            Pais = pais?.Trim()
        };
    }

    /// <summary>Actualiza el nombre y país de la editorial.</summary>
    public void Actualizar(string nombre, string? pais = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre de la editorial es obligatorio.");

        Nombre = nombre.Trim();
        Pais = pais?.Trim();
    }
}
