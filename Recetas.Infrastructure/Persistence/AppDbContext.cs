using Microsoft.EntityFrameworkCore;
using Recetas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recetas.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Receta> Recetas => Set<Receta>();
        public DbSet<Categoria> Categorias => Set<Categoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(e =>
            {
                e.Property(c => c.Nombre).HasMaxLength(80).IsRequired();
                e.HasData(
                    new Categoria { Id = 1, Nombre = "Desayunos" },
                    new Categoria { Id = 2, Nombre = "Almuerzos" },
                    new Categoria { Id = 3, Nombre = "Cenas" },
                    new Categoria { Id = 4, Nombre = "Postres" },
                    new Categoria { Id = 5, Nombre = "Bebidas" },
                    new Categoria { Id = 6, Nombre = "Sopas" },
                    new Categoria { Id = 7, Nombre = "Platillos típicos" }
                );
            });

            modelBuilder.Entity<Receta>(e =>
            {
                e.Property(r => r.Nombre).HasMaxLength(120).IsRequired();
                e.Property(r => r.Descripcion).HasMaxLength(500);
                e.HasOne(r => r.Categoria)
                 .WithMany(c => c.Recetas)
                 .HasForeignKey(r => r.CategoriaId);
            });
        }
    }
}
