using Biblioteca.Domain.Common;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Events;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa una venta de libros a un visitante.
/// Toda la lógica de agregación de detalles, cálculo de totales y anulación vive aquí.
/// </summary>
public sealed class Venta : AuditableEntity
{
    public int VisitanteId { get; private set; }
    public DateTime Fecha { get; private set; }
    public decimal Subtotal { get; private set; }
    public decimal Descuento { get; private set; }
    public decimal Total { get; private set; }
    public EstadoVenta Estado { get; private set; }

    // Navegación
    public Visitante? Visitante { get; private set; }

    private readonly List<DetalleVenta> _detalles = [];
    public IReadOnlyCollection<DetalleVenta> Detalles => _detalles.AsReadOnly();

    private Venta() { }

    /// <summary>Inicia una nueva venta en estado Pendiente.</summary>
    public static Venta Crear(int visitanteId, DateTime fecha, decimal descuento = 0m)
    {
        if (visitanteId <= 0)
            throw new DomainException("El visitante de la venta no es válido.");

        if (descuento < 0)
            throw new DomainException("El descuento no puede ser negativo.");

        return new Venta
        {
            VisitanteId = visitanteId,
            Fecha = fecha,
            Descuento = Math.Round(descuento, 2),
            Subtotal = 0m,
            Total = 0m,
            Estado = EstadoVenta.Pendiente
        };
    }

    /// <summary>
    /// Agrega un detalle a la venta y recalcula los totales automáticamente.
    /// </summary>
    /// <exception cref="DomainException">Si la venta ya está completada o anulada.</exception>
    public void AgregarDetalle(DetalleVenta detalle)
    {
        if (Estado != EstadoVenta.Pendiente)
            throw new DomainException("Solo se pueden agregar detalles a ventas en estado Pendiente.");

        if (detalle is null)
            throw new DomainException("El detalle de venta no puede ser nulo.");

        _detalles.Add(detalle);
        RecalcularTotales();
    }

    /// <summary>
    /// Completa la venta y publica el Domain Event correspondiente.
    /// </summary>
    /// <exception cref="DomainException">Si la venta no tiene detalles.</exception>
    public void Completar(DateTime ahora)
    {
        if (Estado != EstadoVenta.Pendiente)
            throw new DomainException("Solo se pueden completar ventas en estado Pendiente.");

        if (!_detalles.Any())
            throw new DomainException("No se puede completar una venta sin artículos.");

        Estado = EstadoVenta.Completada;

        AddDomainEvent(new VentaRealizadaEvent(
            VentaId: Id,
            VisitanteId: VisitanteId,
            Total: Total,
            CantidadItems: _detalles.Count,
            OcurrioEn: ahora));
    }

    /// <summary>
    /// Anula la venta, impidiendo cualquier modificación posterior.
    /// </summary>
    /// <exception cref="DomainException">Si la venta ya está anulada.</exception>
    public void Anular()
    {
        if (Estado == EstadoVenta.Anulada)
            throw new DomainException("La venta ya está anulada.");

        Estado = EstadoVenta.Anulada;
    }

    /// <summary>Actualiza el descuento y recalcula totales.</summary>
    public void AplicarDescuento(decimal descuento)
    {
        if (Estado != EstadoVenta.Pendiente)
            throw new DomainException("Solo se puede modificar el descuento en ventas pendientes.");

        if (descuento < 0)
            throw new DomainException("El descuento no puede ser negativo.");

        Descuento = Math.Round(descuento, 2);
        RecalcularTotales();
    }

    private void RecalcularTotales()
    {
        Subtotal = Math.Round(_detalles.Sum(d => d.Total), 2);
        Total = Math.Round(Math.Max(0, Subtotal - Descuento), 2);
    }
}
