using System;
using System.Collections.Generic;
using System.Text;

using Recetas.Domain.Entities;

namespace Recetas.Application.Interfaces;

public interface IRecetaRepository
{
    Task<(List<Receta> Items, int Total)> BuscarAsync(string? search, int? categoriaId, int page, int pageSize);
    Task<Receta?> ObtenerPorIdAsync(int id);
    Task<bool> ExisteNombreAsync(string nombre, int? excluirId = null);
    Task<bool> ExisteCategoriaAsync(int categoriaId);
    Task AgregarAsync(Receta receta);
    Task ActualizarAsync(Receta receta);
    Task EliminarAsync(Receta receta);
}
