using Microsoft.EntityFrameworkCore;
using OficinaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURAÇÃO DO BANCO DE DADOS (MySQL com Pomelo)
var connectionString = builder.Configuration.GetConnectionString("OficinaConnection");
builder.Services.AddDbContext<OficinaContext>(opt => opt.UseMySql(
    connectionString,
    ServerVersion.AutoDetect(connectionString)
));

// Adiciona os serviços de Controladores para a API funcionar
builder.Services.AddControllers();
// Vincula a Interface com a sua Implementação Real (Injeção de Dependência)
builder.Services.AddScoped<IOficinaRepo, OficinaRepo>();

// Configuração do Swagger/OpenAPI (útil para testar a API no navegador)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles(); // Procura o index.html na wwwroot
app.UseStaticFiles();  // Permite ler o CSS/JS na wwwroot

app.UseAuthorization();

app.MapControllers(); // O erro acontece aqui se houver DLLs misturadas de EF Core 10 e 9

app.Run();
