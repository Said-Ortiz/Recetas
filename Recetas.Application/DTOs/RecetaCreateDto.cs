using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Recetas.Application.DTOs;

public class RecetaCreateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 120 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La descripción no puede pasar de 500 caracteres.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Los ingredientes son obligatorios.")]
    [StringLength(2000, MinimumLength = 5, ErrorMessage = "Los ingredientes deben tener entre 5 y 2000 caracteres.")]
    public string Ingredientes { get; set; } = string.Empty;

    [Required(ErrorMessage = "La preparación es obligatoria.")]
    [StringLength(4000, MinimumLength = 10, ErrorMessage = "La preparación debe tener entre 10 y 4000 caracteres.")]
    public string Preparacion { get; set; } = string.Empty;

    [Range(1, 1440, ErrorMessage = "El tiempo de preparación debe estar entre 1 y 1440 minutos.")]
    public int TiempoPreparacionMin { get; set; }

    [Range(1, 100, ErrorMessage = "Las porciones deben estar entre 1 y 100.")]
    public int Porciones { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe indicar una categoría válida.")]
    public int CategoriaId { get; set; }
}
