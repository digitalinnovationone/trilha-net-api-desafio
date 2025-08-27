using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "cors_policy",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddDbContext<OrganizadorContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(options =>
        {
            // Informações básicas do documento
            options.DocumentName = "v1";
            options.Title = "Trilha API Desafio";
            options.Version = "v1";
            options.Description = "Aplicação para gerenciar suas tarefas";
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOpenApi();
app.UseSwaggerUi();

app.UseCors("cors_policy");

app.UseHttpsRedirection();

app.MapControllers();

// Pega o serviço do DbContext e aplica quaisquer migrações pendentes
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrganizadorContext>();
    dbContext.Database.Migrate();
}

// Redirect user to /swagger
app.MapGet("/", () => Results.Redirect("/swagger"))
    .ExcludeFromDescription(); // Exclude from Swagger UI

app.Run();