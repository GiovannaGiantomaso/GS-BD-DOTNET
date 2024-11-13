using System.Text.Json.Serialization;
using GlobalSolution.Data;
using GlobalSolution.Repositories.Interfaces;
using GlobalSolution.Repositories.Implementations;
using GlobalSolution.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração da string de conexão
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Registro dos repositórios na injeção de dependência
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IConsumoRepository, ConsumoRepository>();

// Registro dos serviços para chamadas das procedures
builder.Services.AddScoped<UsuarioEnergiaService>(sp =>
    new UsuarioEnergiaService(builder.Configuration.GetConnectionString("OracleConnection")));
builder.Services.AddScoped<ConsumoEnergiaService>(sp =>
    new ConsumoEnergiaService(builder.Configuration.GetConnectionString("OracleConnection")));

// Adicionar serviços ao contêiner com configuração para ignorar ciclos de referência
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
