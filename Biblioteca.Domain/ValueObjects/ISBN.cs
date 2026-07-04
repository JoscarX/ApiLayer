using System.Text.RegularExpressions;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.ValueObjects;

/// <summary>
/// Value Object que representa un ISBN-10 o ISBN-13 válido.
/// </summary>
public sealed class ISBN : IEquatable<ISBN>
{
    private static readonly Regex FormatoIsbn = new(@"^(?:\d{9}[\dX]|\d{13})$", RegexOptions.Compiled);

    public string Valor { get; }

    private ISBN(string valor) => Valor = valor;

    /// <summary>Crea una instancia validada de ISBN.</summary>
    /// <exception cref="DomainException">Si el ISBN no es válido.</exception>
    public static ISBN Crear(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException("El ISBN no puede estar vacío.");

        var normalizado = valor.Replace("-", "").Replace(" ", "").Trim().ToUpper();

        if (!FormatoIsbn.IsMatch(normalizado))
            throw new DomainException($"El ISBN '{valor}' no tiene un formato válido (ISBN-10 o ISBN-13).");

        return new ISBN(normalizado);
    }

    public bool Equals(ISBN? other) => other is not null && Valor == other.Valor;
    public override bool Equals(object? obj) => obj is ISBN other && Equals(other);
    public override int GetHashCode() => Valor.GetHashCode();
    public override string ToString() => Valor;

    public static implicit operator string(ISBN isbn) => isbn.Valor;
}
