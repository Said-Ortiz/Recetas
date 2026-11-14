using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recetas.Application.DTOs;
using Recetas.Application.Interfaces;

namespace Recetas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecetasController : ControllerBase
{
    private readonly IRecetaService _service;

    public RecetasController(IRecetaService service)
    {
        _service = service;
    }

    // GET /api/recetas?search=pollo&categoriaId=2&page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<RecetaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<RecetaDto>>> Get(
        [FromQuery] string? search,
        [FromQuery] int? categoriaId,
        [FromQuery][Range(1, int.MaxValue, ErrorMessage = "page debe ser 1 o mayor.")] int page = 1,
        [FromQuery][Range(1, 50, ErrorMessage = "pageSize debe estar entre 1 y 50.")] int pageSize = 10)
    {
        var resultado = await _service.ListarAsync(search, categoriaId, page, pageSize);
        return Ok(resultado);
    }

    // GET /api/recetas/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RecetaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecetaDto>> GetById(int id)
    {
        var receta = await _service.ObtenerAsync(id);
        return Ok(receta);
    }

    // POST /api/recetas
    [HttpPost]
    [ProducesResponseType(typeof(RecetaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<RecetaDto>> Post([FromBody] RecetaCreateDto dto)
    {
        var creada = await _service.CrearAsync(dto);
        // Devuelve 201 con el encabezado Location apuntando al detalle
        return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
    }

    // PUT /api/recetas/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(int id, [FromBody] RecetaUpdateDto dto)
    {
        await _service.ActualizarAsync(id, dto);
        return NoContent();
    }

    // DELETE /api/recetas/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.EliminarAsync(id);
        return NoContent();
    }
}