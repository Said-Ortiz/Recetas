using System;
using System.Collections.Generic;
using System.Text;

using Recetas.Application.DTOs;
using Recetas.Application.Exceptions;
using Recetas.Application.Interfaces;
using Recetas.Domain.Entities;

namespace Recetas.Application.Services;

public class RecetaService : IRecetaService
{
    private readonly IRecetaRepository _repo;

    public RecetaService(IRecetaRepository repo)
    {
        _repo = repo;
    }

    public async Task<PagedResult<RecetaDto>> ListarAsync(string? search, int? categoriaId, int page, int pageSize)
    {
        var (items, total) = await _repo.BuscarAsync(search, categoriaId, page, pageSize);
        var totalPaginas = (int)Math.Ceiling(total / (double)pageSize);

        return new PagedResult<RecetaDto>(
            items.Select(ToDto).ToList(), page, pageSize, total, totalPaginas);
    }

    public async Task<RecetaDto> ObtenerAsync(int id)
    {
        var receta = await _repo.ObtenerPorIdAsync(id)
            ?? throw new NotFoundException($"No existe una receta con el id {id}.");
        return ToDto(receta);
    }

    public async Task<RecetaDto> CrearAsync(RecetaCreateDto dto)
    {
        var nombre = dto.Nombre.Trim();
        await ValidarReglasAsync(nombre, dto.CategoriaId, null);

        var receta = new Receta
        {
            Nombre = nombre,
            Descripcion = dto.Descripcion?.Trim() ?? string.Empty,
            Ingredientes = dto.Ingredientes.Trim(),
            Preparacion = dto.Preparacion.Trim(),
            TiempoPreparacionMin = dto.TiempoPreparacionMin,
            Porciones = dto.Porciones,
            CategoriaId = dto.CategoriaId,
            FechaCreacion = DateTime.UtcNow
        };

        await _repo.AgregarAsync(receta);

        // Se vuelve a leer para traer el nombre de la categoría
        var creada = await _repo.ObtenerPorIdAsync(receta.Id);
        return ToDto(creada!);
    }

    public async Task ActualizarAsync(int id, RecetaUpdateDto dto)
    {
        var receta = await _repo.ObtenerPorIdAsync(id)
            ?? throw new NotFoundException($"No existe una receta con el id {id}.");

        var nombre = dto.Nombre.Trim();
        await ValidarReglasAsync(nombre, dto.CategoriaId, id);

        receta.Nombre = nombre;
        receta.Descripcion = dto.Descripcion?.Trim() ?? string.Empty;
        receta.Ingredientes = dto.Ingredientes.Trim();
        receta.Preparacion = dto.Preparacion.Trim();
        receta.TiempoPreparacionMin = dto.TiempoPreparacionMin;
        receta.Porciones = dto.Porciones;
        receta.CategoriaId = dto.CategoriaId;

        await _repo.ActualizarAsync(receta);
    }

    public async Task EliminarAsync(int id)
    {
        var receta = await _repo.ObtenerPorIdAsync(id)
            ?? throw new NotFoundException($"No existe una receta con el id {id}.");

        await _repo.EliminarAsync(receta);
    }

    // Reglas de negocio: la categoría debe existir y el nombre no se puede repetir
    private async Task ValidarReglasAsync(string nombre, int categoriaId, int? excluirId)
    {
        if (!await _repo.ExisteCategoriaAsync(categoriaId))
            throw new BadRequestException("La categoría indicada no existe.");

        if (await _repo.ExisteNombreAsync(nombre, excluirId))
            throw new ConflictException($"Ya existe una receta con el nombre '{nombre}'.");
    }

    private static RecetaDto ToDto(Receta r) => new(
        r.Id,
        r.Nombre,
        r.Descripcion,
        r.Ingredientes,
        r.Preparacion,
        r.TiempoPreparacionMin,
        r.Porciones,
        r.FechaCreacion,
        r.CategoriaId,
        r.Categoria?.Nombre ?? string.Empty);
}