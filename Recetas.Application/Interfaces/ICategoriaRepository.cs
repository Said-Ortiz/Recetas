using Recetas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recetas.Application.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObtenerTodasAsync();
    }
}
