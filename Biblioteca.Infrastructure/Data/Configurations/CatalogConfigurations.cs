using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Data.Configurations;

internal sealed class AutorConfiguration : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable("Autores");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nombre)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(a => a.Pais)
            .HasMaxLength(80);
    }
}

internal sealed class EditorialConfiguration : IEntityTypeConfiguration<Editorial>
{
    public void Configure(EntityTypeBuilder<Editorial> builder)
    {
        builder.ToTable("Editoriales");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nombre)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Pais)
            .HasMaxLength(80);
            
        builder.HasIndex(e => e.Nombre).IsUnique();
    }
}

internal sealed class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.HasIndex(c => c.Nombre).IsUnique();
    }
}

internal sealed class LibroConfiguration : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.ToTable("Libros");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Nombre)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(l => l.Isbn)
            .HasMaxLength(20);

        builder.Property(l => l.Precio)
            .HasPrecision(18, 2);

        builder.HasIndex(l => l.Isbn).IsUnique().HasFilter("[Isbn] IS NOT NULL");

        builder.HasOne(l => l.Autor)
            .WithMany(a => a.Libros)
            .HasForeignKey(l => l.AutorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Editorial)
            .WithMany(e => e.Libros)
            .HasForeignKey(l => l.EditorialId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.Categorias)
            .WithMany(c => c.Libros)
            .UsingEntity(j => j.ToTable("LibroCategoria"));
    }
}
