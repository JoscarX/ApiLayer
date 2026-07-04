namespace Biblioteca.Domain.Enums;

/// <summary>Estado del préstamo de un libro.</summary>
public enum EstadoPrestamo : byte
{
    Activo = 1,
    Devuelto = 2,
    Vencido = 3
}
