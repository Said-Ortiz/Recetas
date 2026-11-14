using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Recetas.Application.Interfaces;
using Recetas.Domain.Entities;
using Recetas.Infrastructure.Persistence;

namespace Recetas.Infrastructure.Repositories;

public class RecetaRepository : IRecetaRepository
{
    private readonly AppDbContext _context;

    public RecetaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(List<Receta> Items, int Total)> BuscarAsync(
        string? search, int? categoriaId, int page, int pageSize)
    {
        var query = _context.Recetas
            .AsNoTracking()
            .Include(r => r.Categoria)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var texto = search.Trim();
            query = query.Where(r =>
                r.Nombre.Contains(texto) ||
                r.Descripcion.Contains(texto) ||
                r.Ingredientes.Contains(texto));
        }

        if (categoriaId.HasValue)
            query = query.Where(r => r.CategoriaId == categoriaId.Value);

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(r => r.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

    public async Task<Receta?> ObtenerPorIdAsync(int id)
    {
        return await _context.Recetas
            .Include(r => r.Categoria)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> ExisteNombreAsync(string nombre, int? excluirId = null)
    {
        return await _context.Recetas.AnyAsync(r =>
            r.Nombre == nombre && (excluirId == null || r.Id != excluirId));
    }

    public async Task<bool> ExisteCategoriaAsync(int categoriaId)
    {
        return await _context.Categorias.AnyAsync(c => c.Id == categoriaId);
    }

    public async Task AgregarAsync(Receta receta)
    {
        _context.Recetas.Add(receta);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Receta receta)
    {
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(Receta receta)
    {
        _context.Recetas.Remove(receta);
        await _context.SaveChangesAsync();
    }
}
