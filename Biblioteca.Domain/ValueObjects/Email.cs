using System.Text.RegularExpressions;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.ValueObjects;

/// <summary>
/// Value Object que representa una dirección de correo electrónico válida.
/// </summary>
public sealed class Email : IEquatable<Email>
{
    private static readonly Regex FormatoEmail = new(
        @"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Valor { get; }

    private Email(string valor) => Valor = valor.ToLowerInvariant();

    /// <summary>Crea una instancia validada de Email.</summary>
    /// <exception cref="DomainException">Si el formato del correo es inválido.</exception>
    public static Email Crear(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException("El correo electrónico no puede estar vacío.");

        if (!FormatoEmail.IsMatch(valor.Trim()))
            throw new DomainException($"El formato del correo '{valor}' es inválido.");

        return new Email(valor.Trim());
    }

    public bool Equals(Email? other) => other is not null && Valor == other.Valor;
    public override bool Equals(object? obj) => obj is Email other && Equals(other);
    public override int GetHashCode() => Valor.GetHashCode();
    public override string ToString() => Valor;

    public static implicit operator string(Email email) => email.Valor;
}
