namespace Biblioteca.Domain.Exceptions;

/// <summary>Se lanza cuando no hay stock suficiente para una operación de venta.</summary>
public sealed class StockInsuficienteException : DomainException
{
    public StockInsuficienteException(string titulo, int disponible, int solicitado)
        : base($"Stock insuficiente para '{titulo}'. Disponible: {disponible}, Solicitado: {solicitado}.") { }
}
