using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ProdutoCategoria.Application.Services;
using ProdutoCategoria.Domain.Interfaces.Repositories;
using ProdutoCategoria.Infra.Context;
using ProdutoCategoria.Infra.Repositories;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// 1. REGISTRO DOS CONTROLLERS (A linha que faltava!)
builder.Services.AddControllers();

// 2. Configuração do MySQL com EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);



// 3. Injeção de Dependência (DI) - Repositories
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// 4. Injeção de Dependência (DI) - Services (Application)
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoService>();

// 5. Configuração do OpenAPI (Necessário para o Scalar funcionar)
builder.Services.AddOpenApi();

var app = builder.Build();

// 6. Mapeamento do Endpoint do OpenAPI
app.MapOpenApi();

// 7. Configuração do Scalar UI
app.MapScalarApiReference("/docs", options =>
{
    options
        .WithTitle("ProdutoCategoria DDD API")
        .WithTheme(ScalarTheme.Purple);
});

app.UseHttpsRedirection();

// 8. Mapeamento dos Endpoints dos Controllers
app.MapControllers();

app.Run();