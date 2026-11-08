using Microsoft.EntityFrameworkCore;
using Recetas.Application.Interfaces;
using Recetas.Domain.Entities;
using Recetas.Infrastructure.Persistence;

namespace Recetas.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> ObtenerTodasAsync()
    {
        return await _context.Categorias
            .AsNoTracking()
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }
}
