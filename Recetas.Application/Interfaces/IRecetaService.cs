using System;
using System.Collections.Generic;
using System.Text;

using Recetas.Application.DTOs;

namespace Recetas.Application.Interfaces;

public interface IRecetaService
{
    Task<PagedResult<RecetaDto>> ListarAsync(string? search, int? categoriaId, int page, int pageSize);
    Task<RecetaDto> ObtenerAsync(int id);
    Task<RecetaDto> CrearAsync(RecetaCreateDto dto);
    Task ActualizarAsync(int id, RecetaUpdateDto dto);
    Task EliminarAsync(int id);
}
