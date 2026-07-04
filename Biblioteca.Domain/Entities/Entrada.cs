using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa el registro de entrada y salida de un visitante a la biblioteca.
/// </summary>
public sealed class Entrada : AuditableEntity
{
    public int VisitanteId { get; private set; }
    public DateTime FechaEntrada { get; private set; }
    public DateTime? FechaSalida { get; private set; }

    // Navegación
    public Visitante? Visitante { get; private set; }

    private Entrada() { }

    /// <summary>Registra una nueva entrada de un visitante.</summary>
    public static Entrada Crear(int visitanteId, DateTime fechaEntrada)
    {
        if (visitanteId <= 0)
            throw new DomainException("El visitante de la entrada no es válido.");

        return new Entrada
        {
            VisitanteId = visitanteId,
            FechaEntrada = fechaEntrada
        };
    }

    /// <summary>
    /// Registra la salida del visitante.
    /// </summary>
    /// <exception cref="DomainException">Si la fecha de salida es anterior a la entrada.</exception>
    public void RegistrarSalida(DateTime fechaSalida)
    {
        if (FechaSalida.HasValue)
            throw new DomainException("La salida de esta entrada ya fue registrada.");

        if (fechaSalida < FechaEntrada)
            throw new DomainException("La fecha de salida no puede ser anterior a la fecha de entrada.");

        FechaSalida = fechaSalida;
    }

    /// <summary>Indica si el visitante aún está dentro de la biblioteca.</summary>
    public bool EstaActiva => !FechaSalida.HasValue;

    /// <summary>Calcula la duración de la visita si ya salió.</summary>
    public TimeSpan? DuracionVisita => FechaSalida.HasValue
        ? FechaSalida.Value - FechaEntrada
        : null;
}
