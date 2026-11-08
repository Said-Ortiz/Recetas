using System;
using System.Collections.Generic;
using System.Text;

namespace Recetas.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public List<Receta> Recetas { get; set; } = new();
    }
}
