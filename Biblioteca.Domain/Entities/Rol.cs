using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa un rol del sistema (Administrador, Bibliotecario, etc.).
/// </summary>
public sealed class Rol : Entity
{
    public string Nombre { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }

    // Navegación
    private readonly List<Usuario> _usuarios = [];
    public IReadOnlyCollection<Usuario> Usuarios => _usuarios.AsReadOnly();

    private Rol() { }

    /// <summary>Crea un nuevo rol con nombre y descripción opcionales.</summary>
    public static Rol Crear(string nombre, string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del rol es obligatorio.");

        if (nombre.Length > 50)
            throw new DomainException("El nombre del rol no puede exceder 50 caracteres.");

        return new Rol
        {
            Nombre = nombre.Trim(),
            Descripcion = descripcion?.Trim()
        };
    }

    /// <summary>Actualiza el nombre y descripción del rol.</summary>
    public void Actualizar(string nombre, string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del rol es obligatorio.");

        Nombre = nombre.Trim();
        Descripcion = descripcion?.Trim();
    }
}
