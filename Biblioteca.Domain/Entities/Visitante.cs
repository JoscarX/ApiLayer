using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa un visitante de la biblioteca.
/// Persona que puede realizar préstamos, ventas y registrar entradas.
/// </summary>
public sealed class Visitante : AuditableEntity
{
    public string NombreCompleto { get; private set; } = string.Empty;
    public string Cedula { get; private set; } = string.Empty;
    public string? Correo { get; private set; }
    public string Telefono { get; private set; } = string.Empty;
    public string Direccion { get; private set; } = string.Empty;

    // Navegación
    private readonly List<Prestamo> _prestamos = [];
    private readonly List<Venta> _ventas = [];
    private readonly List<Entrada> _entradas = [];

    public IReadOnlyCollection<Prestamo> Prestamos => _prestamos.AsReadOnly();
    public IReadOnlyCollection<Venta> Ventas => _ventas.AsReadOnly();
    public IReadOnlyCollection<Entrada> Entradas => _entradas.AsReadOnly();

    private Visitante() { }

    /// <summary>Crea un nuevo visitante con sus datos personales.</summary>
    public static Visitante Crear(
        string nombreCompleto,
        string cedula,
        string telefono,
        string direccion,
        string? correo = null)
    {
        ValidarNombre(nombreCompleto);
        ValidarCedula(cedula);
        ValidarTelefono(telefono);
        ValidarDireccion(direccion);

        return new Visitante
        {
            NombreCompleto = nombreCompleto.Trim(),
            Cedula = cedula.Trim(),
            Telefono = telefono.Trim(),
            Direccion = direccion.Trim(),
            Correo = correo?.Trim().ToLowerInvariant()
        };
    }

    /// <summary>Actualiza los datos personales del visitante.</summary>
    public void ActualizarDatos(
        string nombreCompleto,
        string telefono,
        string direccion,
        string? correo = null)
    {
        ValidarNombre(nombreCompleto);
        ValidarTelefono(telefono);
        ValidarDireccion(direccion);

        NombreCompleto = nombreCompleto.Trim();
        Telefono = telefono.Trim();
        Direccion = direccion.Trim();
        Correo = correo?.Trim().ToLowerInvariant();
    }

    private static void ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre completo del visitante es obligatorio.");

        if (nombre.Length > 120)
            throw new DomainException("El nombre no puede exceder 120 caracteres.");
    }

    private static void ValidarCedula(string cedula)
    {
        if (string.IsNullOrWhiteSpace(cedula))
            throw new DomainException("La cédula del visitante es obligatoria.");
    }

    private static void ValidarTelefono(string telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
            throw new DomainException("El teléfono del visitante es obligatorio.");
    }

    private static void ValidarDireccion(string direccion)
    {
        if (string.IsNullOrWhiteSpace(direccion))
            throw new DomainException("La dirección del visitante es obligatoria.");
    }
}
