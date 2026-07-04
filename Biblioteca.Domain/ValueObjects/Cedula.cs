using System.Text.RegularExpressions;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.ValueObjects;

/// <summary>
/// Value Object que representa una cédula de identidad.
/// Encapsula la validación de formato y garantiza su inmutabilidad.
/// </summary>
public sealed class Cedula : IEquatable<Cedula>
{
    private static readonly Regex FormatoCedula = new(@"^\d{3}-?\d{7}-?\d{1}$", RegexOptions.Compiled);

    public string Valor { get; }

    private Cedula(string valor) => Valor = valor;

    /// <summary>Crea una instancia validada de Cedula.</summary>
    /// <exception cref="DomainException">Si el formato de la cédula es inválido.</exception>
    public static Cedula Crear(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException("La cédula no puede estar vacía.");

        var normalizada = valor.Replace("-", "").Trim();

        if (normalizada.Length != 11 || !normalizada.All(char.IsDigit))
            throw new DomainException($"El formato de la cédula '{valor}' es inválido.");

        return new Cedula(normalizada);
    }

    public bool Equals(Cedula? other) => other is not null && Valor == other.Valor;
    public override bool Equals(object? obj) => obj is Cedula other && Equals(other);
    public override int GetHashCode() => Valor.GetHashCode();
    public override string ToString() => Valor;

    public static implicit operator string(Cedula cedula) => cedula.Valor;
}
