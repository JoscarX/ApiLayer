using Biblioteca.Domain.Common;
using Biblioteca.Domain.Enums;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Registro de auditoría inmutable. Solo se crea, nunca se modifica.
/// Captura el estado antes y después de cada operación relevante.
/// </summary>
public sealed class Auditoria : Entity
{
    public string Tabla { get; private set; } = string.Empty;
    public AccionAuditoria Accion { get; private set; }
    public int IdRegistro { get; private set; }
    public string? DatosAntes { get; private set; }
    public string? DatosDespues { get; private set; }
    public int? UsuarioId { get; private set; }
    public DateTime Fecha { get; private set; }

    // Navegación
    public Usuario? Usuario { get; private set; }

    private Auditoria() { }

    /// <summary>
    /// Crea un registro de auditoría inmutable.
    /// </summary>
    public static Auditoria Crear(
        string tabla,
        AccionAuditoria accion,
        int idRegistro,
        DateTime fecha,
        int? usuarioId = null,
        string? datosAntes = null,
        string? datosDespues = null)
    {
        return new Auditoria
        {
            Tabla = tabla,
            Accion = accion,
            IdRegistro = idRegistro,
            Fecha = fecha,
            UsuarioId = usuarioId,
            DatosAntes = datosAntes,
            DatosDespues = datosDespues
        };
    }
}
