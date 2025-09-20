using CandidatosData;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CandidatosBusiness; // novo namespace sugerido para o servi�o

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        // Serializa��o previs�vel (camelCase, datas ISO)
        o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        o.JsonSerializerOptions.WriteIndented = true;
    });

// OpenAPI/Swagger
builder.Services.AddOpenApi();

// DbContext (Oracle)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS (opcional, mas �til em testes)
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(p => p
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// DI do dom�nio novo
builder.Services.AddScoped<IApostaSiteService, ApostaSiteService>();

var app = builder.Build();

// Dev-only OpenAPI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
