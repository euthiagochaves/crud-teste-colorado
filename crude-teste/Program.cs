using crude_teste.Data;
using crude_teste.Domain.Interfaces;
using crude_teste.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add controllers com opções de JSON
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

//ESSENCIAL PRO SWAGGER FUNCIONAR
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cliente CRUD API",
        Version = "v1",
        Description = "API RESTful para gerenciamento de clientes"
    });
});

// Banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ITipoTelefoneRepository, TipoTelefoneRepository>();

var app = builder.Build();

// SWAGGER ATIVADO SEM CONDIÇÃO DE DEV
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cliente CRUD API v1");
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// MVC padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed do banco
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        if (!context.TiposTelefone.Any())
        {
            context.TiposTelefone.AddRange(
                new crude_teste.Models.TipoTelefone
                {
                    CodigoTipoTelefone = 1,
                    DescricaoTipoTelefone = "RESIDENCIAL",
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = "SYSTEM"
                },
                new crude_teste.Models.TipoTelefone
                {
                    CodigoTipoTelefone = 2,
                    DescricaoTipoTelefone = "COMERCIAL",
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = "SYSTEM"
                },
                new crude_teste.Models.TipoTelefone
                {
                    CodigoTipoTelefone = 3,
                    DescricaoTipoTelefone = "WHATSAPP",
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = "SYSTEM"
                },
                new crude_teste.Models.TipoTelefone
                {
                    CodigoTipoTelefone = 4,
                    DescricaoTipoTelefone = "CELULAR",
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = "SYSTEM"
                }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao inicializar o banco de dados.");
    }
}

app.Run();
