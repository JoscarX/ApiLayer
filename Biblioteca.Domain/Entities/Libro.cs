using Biblioteca.Domain.Common;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Entidad central del sistema. Representa un libro de la biblioteca.
/// Encapsula toda la lógica de stock, préstamos, devoluciones y precio.
/// </summary>
public sealed class Libro : AuditableEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public string? Sinopsis { get; private set; }
    public int Paginas { get; private set; }
    public int CantidadDisponible { get; private set; }
    public decimal Precio { get; private set; }
    public string? Isbn { get; private set; }
    public int AutorId { get; private set; }
    public int EditorialId { get; private set; }

    // Navegación
    public Autor? Autor { get; private set; }
    public Editorial? Editorial { get; private set; }

    private readonly List<Categoria> _categorias = [];
    private readonly List<Prestamo> _prestamos = [];
    private readonly List<DetalleVenta> _detallesVenta = [];

    public IReadOnlyCollection<Categoria> Categorias => _categorias.AsReadOnly();
    public IReadOnlyCollection<Prestamo> Prestamos => _prestamos.AsReadOnly();
    public IReadOnlyCollection<DetalleVenta> DetallesVenta => _detallesVenta.AsReadOnly();

    private Libro() { }

    /// <summary>Crea un nuevo libro con sus datos básicos.</summary>
    public static Libro Crear(
        string nombre,
        int paginas,
        int cantidadInicial,
        decimal precio,
        int autorId,
        int editorialId,
        string? sinopsis = null,
        string? isbn = null)
    {
        ValidarNombre(nombre);
        ValidarPaginas(paginas);
        ValidarCantidad(cantidadInicial);
        ValidarPrecio(precio);

        if (autorId <= 0)
            throw new DomainException("El autor del libro no es válido.");

        if (editorialId <= 0)
            throw new DomainException("La editorial del libro no es válida.");

        return new Libro
        {
            Nombre = nombre.Trim(),
            Paginas = paginas,
            CantidadDisponible = cantidadInicial,
            Precio = precio,
            AutorId = autorId,
            EditorialId = editorialId,
            Sinopsis = sinopsis?.Trim(),
            Isbn = isbn?.Trim()
        };
    }

    /// <summary>
    /// Reduce el stock en 1 unidad para un préstamo.
    /// Lanza excepción si no hay disponibilidad.
    /// </summary>
    /// <exception cref="LibroNoDisponibleException">Si no hay unidades disponibles.</exception>
    public void Prestar()
    {
        if (CantidadDisponible <= 0)
            throw new LibroNoDisponibleException(Nombre);

        CantidadDisponible--;
    }

    /// <summary>
    /// Incrementa el stock en 1 unidad al devolver un préstamo.
    /// </summary>
    public void Devolver()
    {
        CantidadDisponible++;
    }

    /// <summary>
    /// Reduce el stock en la cantidad especificada para una venta.
    /// Lanza excepción si el stock es insuficiente.
    /// </summary>
    /// <exception cref="StockInsuficienteException">Si no hay suficientes unidades.</exception>
    public void ReducirStockParaVenta(int cantidad)
    {
        if (cantidad <= 0)
            throw new DomainException("La cantidad a reducir debe ser mayor a cero.");

        if (CantidadDisponible < cantidad)
            throw new StockInsuficienteException(Nombre, CantidadDisponible, cantidad);

        CantidadDisponible -= cantidad;
    }

    /// <summary>Agrega existencias al inventario del libro.</summary>
    public void AgregarExistencias(int cantidad)
    {
        if (cantidad <= 0)
            throw new DomainException("La cantidad a agregar debe ser mayor a cero.");

        CantidadDisponible += cantidad;
    }

    /// <summary>Actualiza el precio de venta del libro.</summary>
    public void ActualizarPrecio(decimal nuevoPrecio)
    {
        ValidarPrecio(nuevoPrecio);
        Precio = nuevoPrecio;
    }

    /// <summary>Actualiza los datos descriptivos del libro.</summary>
    public void Actualizar(
        string nombre,
        int paginas,
        decimal precio,
        int autorId,
        int editorialId,
        string? sinopsis = null,
        string? isbn = null)
    {
        ValidarNombre(nombre);
        ValidarPaginas(paginas);
        ValidarPrecio(precio);

        Nombre = nombre.Trim();
        Paginas = paginas;
        Precio = precio;
        AutorId = autorId;
        EditorialId = editorialId;
        Sinopsis = sinopsis?.Trim();
        Isbn = isbn?.Trim();
    }

    private static void ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del libro es obligatorio.");

        if (nombre.Length > 120)
            throw new DomainException("El nombre del libro no puede exceder 120 caracteres.");
    }

    private static void ValidarPaginas(int paginas)
    {
        if (paginas <= 0)
            throw new DomainException("El número de páginas debe ser mayor a cero.");
    }

    private static void ValidarCantidad(int cantidad)
    {
        if (cantidad < 0)
            throw new DomainException("La cantidad disponible no puede ser negativa.");
    }

    private static void ValidarPrecio(decimal precio)
    {
        if (precio < 0)
            throw new DomainException("El precio del libro no puede ser negativo.");
    }
}
