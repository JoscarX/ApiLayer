using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa un usuario del sistema. Encapsula sus credenciales, rol y estado.
/// Toda la lógica de activación y cambio de contraseña vive aquí.
/// </summary>
public sealed class Usuario : AuditableEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public string Cedula { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public int RolId { get; private set; }
    public bool Activo { get; private set; }

    // Navegación
    public Rol? Rol { get; private set; }

    private Usuario() { }

    /// <summary>Crea un nuevo usuario con sus credenciales y rol asignado.</summary>
    public static Usuario Crear(string nombre, string cedula, string passwordHash, int rolId)
    {
        ValidarNombre(nombre);
        ValidarCedula(cedula);

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("El hash de contraseña no puede estar vacío.");

        if (rolId <= 0)
            throw new DomainException("El rol asignado no es válido.");

        return new Usuario
        {
            Nombre = nombre.Trim(),
            Cedula = cedula.Trim(),
            PasswordHash = passwordHash,
            RolId = rolId,
            Activo = true
        };
    }

    /// <summary>Actualiza el hash de contraseña del usuario.</summary>
    /// <exception cref="UsuarioInactivoException">Si el usuario está inactivo.</exception>
    public void CambiarPassword(string nuevoHash)
    {
        if (!Activo)
            throw new UsuarioInactivoException(Nombre);

        if (string.IsNullOrWhiteSpace(nuevoHash))
            throw new DomainException("La nueva contraseña no puede estar vacía.");

        PasswordHash = nuevoHash;
    }

    /// <summary>Activa el usuario en el sistema.</summary>
    public void Activar()
    {
        if (Activo) return;
        Activo = true;
    }

    /// <summary>Desactiva el usuario, impidiendo su acceso al sistema.</summary>
    public void Desactivar()
    {
        if (!Activo) return;
        Activo = false;
    }

    /// <summary>Actualiza los datos del perfil del usuario.</summary>
    public void ActualizarPerfil(string nombre, int rolId)
    {
        ValidarNombre(nombre);

        if (rolId <= 0)
            throw new DomainException("El rol asignado no es válido.");

        Nombre = nombre.Trim();
        RolId = rolId;
    }

    private static void ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del usuario es obligatorio.");

        if (nombre.Length > 100)
            throw new DomainException("El nombre no puede exceder 100 caracteres.");
    }

    private static void ValidarCedula(string cedula)
    {
        if (string.IsNullOrWhiteSpace(cedula))
            throw new DomainException("La cédula es obligatoria.");

        if (cedula.Length > 20)
            throw new DomainException("La cédula no puede exceder 20 caracteres.");
    }
}
