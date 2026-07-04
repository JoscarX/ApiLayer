using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Detalle de una venta: un libro vendido, su cantidad y precio al momento de la venta.
/// </summary>
public sealed class DetalleVenta : Entity
{
    public int VentaId { get; private set; }
    public int LibroId { get; private set; }
    public int Cantidad { get; private set; }
    public decimal Precio { get; private set; }
    public decimal Total { get; private set; }

    // Navegación
    public Venta? Venta { get; private set; }
    public Libro? Libro { get; private set; }

    private DetalleVenta() { }

    /// <summary>
    /// Crea un detalle de venta calculando el total automáticamente.
    /// </summary>
    public static DetalleVenta Crear(int libroId, int cantidad, decimal precio)
    {
        if (libroId <= 0)
            throw new DomainException("El libro del detalle de venta no es válido.");

        if (cantidad <= 0)
            throw new DomainException("La cantidad en el detalle de venta debe ser mayor a cero.");

        if (precio < 0)
            throw new DomainException("El precio en el detalle de venta no puede ser negativo.");

        return new DetalleVenta
        {
            LibroId = libroId,
            Cantidad = cantidad,
            Precio = precio,
            Total = CalcularTotal(precio, cantidad)
        };
    }

    /// <summary>Calcula el total del detalle (precio × cantidad).</summary>
    private static decimal CalcularTotal(decimal precio, int cantidad) =>
        Math.Round(precio * cantidad, 2);
}
