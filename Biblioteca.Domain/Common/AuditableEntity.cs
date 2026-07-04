namespace Biblioteca.Domain.Common;

/// <summary>
/// Extiende Entity con campos de auditoría estándar (quién y cuándo creó/modificó).
/// </summary>
public abstract class AuditableEntity : Entity
{
    /// <summary>Fecha y hora de creación del registro.</summary>
    public DateTime FechaCreacion { get; private set; }

    /// <summary>Fecha y hora de la última modificación del registro.</summary>
    public DateTime? FechaModificacion { get; private set; }

    /// <summary>Id del usuario que creó el registro.</summary>
    public int? CreadoPor { get; private set; }

    /// <summary>Id del usuario que realizó la última modificación.</summary>
    public int? ModificadoPor { get; private set; }

    /// <summary>Establece los datos de auditoría al momento de la creación.</summary>
    public void EstablecerCreacion(DateTime fecha, int? usuarioId = null)
    {
        FechaCreacion = fecha;
        CreadoPor = usuarioId;
    }

    /// <summary>Establece los datos de auditoría al momento de una modificación.</summary>
    public void EstablecerModificacion(DateTime fecha, int? usuarioId = null)
    {
        FechaModificacion = fecha;
        ModificadoPor = usuarioId;
    }
}
