using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa el autor de uno o más libros.
/// </summary>
public sealed class Autor : AuditableEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public string? Biografia { get; private set; }
    public string? Pais { get; private set; }
    public DateOnly? FechaNacimiento { get; private set; }

    // Navegación
    private readonly List<Libro> _libros = [];
    public IReadOnlyCollection<Libro> Libros => _libros.AsReadOnly();

    private Autor() { }

    /// <summary>Crea un nuevo autor.</summary>
    public static Autor Crear(
        string nombre,
        string? biografia = null,
        string? pais = null,
        DateOnly? fechaNacimiento = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del autor es obligatorio.");

        if (nombre.Length > 120)
            throw new DomainException("El nombre del autor no puede exceder 120 caracteres.");

        return new Autor
        {
            Nombre = nombre.Trim(),
            Biografia = biografia?.Trim(),
            Pais = pais?.Trim(),
            FechaNacimiento = fechaNacimiento
        };
    }

    /// <summary>Actualiza la información biográfica del autor.</summary>
    public void ActualizarInformacion(
        string nombre,
        string? biografia = null,
        string? pais = null,
        DateOnly? fechaNacimiento = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del autor es obligatorio.");

        Nombre = nombre.Trim();
        Biografia = biografia?.Trim();
        Pais = pais?.Trim();
        FechaNacimiento = fechaNacimiento;
    }
}
