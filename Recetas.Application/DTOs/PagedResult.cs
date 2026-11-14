using System;
using System.Collections.Generic;
using System.Text;

namespace Recetas.Application.DTOs;

public record PagedResult<T>(
    List<T> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);
