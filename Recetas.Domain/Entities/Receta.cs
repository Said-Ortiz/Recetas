using System;
using System.Collections.Generic;
using System.Text;

namespace Recetas.Domain.Entities
{
    public class Receta
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Ingredientes { get; set; } = string.Empty;
        public string Preparacion { get; set; } = string.Empty;
        public int TiempoPreparacionMin { get; set; }
        public int Porciones { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
