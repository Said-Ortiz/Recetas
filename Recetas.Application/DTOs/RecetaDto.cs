using System;
using System.Collections.Generic;
using System.Text;

namespace Recetas.Application.DTOs;

public record RecetaDto(
    int Id,
    string Nombre,
    string Descripcion,
    string Ingredientes,
    string Preparacion,
    int TiempoPreparacionMin,
    int Porciones,
    DateTime FechaCreacion,
    int CategoriaId,
    string Categoria);
