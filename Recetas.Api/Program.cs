using Recetas.Application.DTOs;
using Recetas.Application.Interfaces;
using Recetas.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/api/categorias", async (ICategoriaRepository repo) =>
{
    var categorias = await repo.ObtenerTodasAsync();
    return Results.Ok(categorias.Select(c => new CategoriaDto(c.Id, c.Nombre)));
});

app.Run();