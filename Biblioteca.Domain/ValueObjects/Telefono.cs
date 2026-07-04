using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.ValueObjects;

/// <summary>
/// Value Object que representa un número de teléfono.
/// </summary>
public sealed class Telefono : IEquatable<Telefono>
{
    public string Valor { get; }

    private Telefono(string valor) => Valor = valor;

    /// <summary>Crea una instancia validada de Telefono.</summary>
    /// <exception cref="DomainException">Si el número de teléfono es inválido.</exception>
    public static Telefono Crear(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException("El número de teléfono no puede estar vacío.");

        var normalizado = valor.Replace("-", "").Replace(" ", "").Replace("+", "").Trim();

        if (normalizado.Length < 7 || normalizado.Length > 15 || !normalizado.All(char.IsDigit))
            throw new DomainException($"El número de teléfono '{valor}' es inválido.");

        return new Telefono(valor.Trim());
    }

    public bool Equals(Telefono? other) => other is not null && Valor == other.Valor;
    public override bool Equals(object? obj) => obj is Telefono other && Equals(other);
    public override int GetHashCode() => Valor.GetHashCode();
    public override string ToString() => Valor;

    public static implicit operator string(Telefono telefono) => telefono.Valor;
}
