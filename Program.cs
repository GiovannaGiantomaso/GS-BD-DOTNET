using System.Text.Json.Serialization;
using GlobalSolution.Data;
using GlobalSolution.Repositories.Interfaces;
using GlobalSolution.Repositories.Implementations;
using GlobalSolution.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura��o da string de conex�o
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Registro dos reposit�rios na inje��o de depend�ncia
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IConsumoRepository, ConsumoRepository>();
builder.Services.AddScoped<IHistoricoConsumoRepository, HistoricoConsumoRepository>();
builder.Services.AddScoped<IFeedbackConsumoRepository, FeedbackConsumoRepository>();

// Registro dos servi�os para chamadas das procedures e reposit�rios
builder.Services.AddScoped<UsuarioEnergiaService>(sp =>
    new UsuarioEnergiaService(builder.Configuration.GetConnectionString("OracleConnection")));
builder.Services.AddScoped<ConsumoEnergiaService>(sp =>
    new ConsumoEnergiaService(builder.Configuration.GetConnectionString("OracleConnection")));
builder.Services.AddScoped<HistoricoConsumoService>(sp =>
    new HistoricoConsumoService(
        builder.Configuration.GetConnectionString("OracleConnection"),
        sp.GetRequiredService<IHistoricoConsumoRepository>() // Injeta o reposit�rio para opera��es de banco
    ));
builder.Services.AddScoped<FeedbackConsumoService>(sp =>
    new FeedbackConsumoService(
        builder.Configuration.GetConnectionString("OracleConnection"),
        sp.GetRequiredService<IFeedbackConsumoRepository>() // Injeta o reposit�rio para opera��es de banco
    ));

// Adicionar servi�os ao cont�iner com configura��o para ignorar ciclos de refer�ncia no JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Ignora ciclos de refer�ncia para evitar erros de serializa��o
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Ignora valores nulos ao serializar JSON
});

// Adicionar endpoints e documenta��o com Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura��o do pipeline de requisi��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
