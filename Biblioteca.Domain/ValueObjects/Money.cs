using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.ValueObjects;

/// <summary>
/// Value Object que representa un valor monetario siempre no negativo.
/// Garantiza la integridad financiera en el dominio.
/// </summary>
public sealed class Money : IEquatable<Money>, IComparable<Money>
{
    public decimal Cantidad { get; }

    private Money(decimal cantidad) => Cantidad = cantidad;

    /// <summary>Crea una instancia validada de Money.</summary>
    /// <exception cref="DomainException">Si el valor es negativo.</exception>
    public static Money Crear(decimal cantidad)
    {
        if (cantidad < 0)
            throw new DomainException($"El valor monetario no puede ser negativo: {cantidad}.");

        return new Money(Math.Round(cantidad, 2));
    }

    public static Money Cero => new(0m);

    public Money Sumar(Money otro) => new(Cantidad + otro.Cantidad);

    public Money Restar(Money otro)
    {
        if (Cantidad < otro.Cantidad)
            throw new DomainException("El resultado de la resta sería un valor monetario negativo.");

        return new Money(Cantidad - otro.Cantidad);
    }

    public Money Multiplicar(int factor)
    {
        if (factor < 0)
            throw new DomainException("El factor de multiplicación no puede ser negativo.");

        return new Money(Cantidad * factor);
    }

    public bool Equals(Money? other) => other is not null && Cantidad == other.Cantidad;
    public override bool Equals(object? obj) => obj is Money other && Equals(other);
    public override int GetHashCode() => Cantidad.GetHashCode();
    public int CompareTo(Money? other) => other is null ? 1 : Cantidad.CompareTo(other.Cantidad);
    public override string ToString() => Cantidad.ToString("F2");

    public static implicit operator decimal(Money money) => money.Cantidad;
    public static Money operator +(Money a, Money b) => a.Sumar(b);
    public static Money operator -(Money a, Money b) => a.Restar(b);
    public static bool operator >(Money a, Money b) => a.Cantidad > b.Cantidad;
    public static bool operator <(Money a, Money b) => a.Cantidad < b.Cantidad;
    public static bool operator >=(Money a, Money b) => a.Cantidad >= b.Cantidad;
    public static bool operator <=(Money a, Money b) => a.Cantidad <= b.Cantidad;
}
