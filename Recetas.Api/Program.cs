using Recetas.Api.Handlers;
using Recetas.Application.DTOs;
using Recetas.Application.Interfaces;
using Recetas.Application.Services;
using Recetas.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IRecetaService, RecetaService>();

var app = builder.Build();

app.UseExceptionHandler();

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

app.MapControllers();

app.Run();