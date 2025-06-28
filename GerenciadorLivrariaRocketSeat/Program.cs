using GerenciadorLivrariaRocketSeat.DataBase;
using GerenciadorLivrariaRocketSeat.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Obtém a versão do assembly da API
Version version = Assembly.GetEntryAssembly()?.GetName().Version!;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // Aqui adiciona o seu filtro customizado
    options.OperationFilter<ExamplesRequestResponseFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gerenciador de Livraria - API",
        Version = $"{version}",
        Description = "Gerenciador de Livraria",

        TermsOfService = new Uri("https://github.com/rodrigoodilon/DesafioRocketSeat1"),
        Contact = new OpenApiContact
        {
            Name = "Rodrigo Odilon Mariano da Silva",
            Email = "rodrigo.odilon@hotmail.com",
        },
        License = new OpenApiLicense
        {
            Name = "MIT License ©️ 2025 Rodrigo Odilon",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

/* Configura um rate limiter com política específica para a rota de inserção de livros ("BooksInsertPolicy").
 * Permite até 5 requisições por IP a cada 20 segundos, sem permitir fila de espera.
 * Requisições além do limite são imediatamente rejeitadas.*/
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("BooksInsertPolicy", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromSeconds(20),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));
});

var app = builder.Build();

app.UseRateLimiter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Cria as colunas do DataTable para armazenar os livros
MainDataBase.ListBooks.Columns.Add("Id", typeof(int));
MainDataBase.ListBooks.Columns.Add("Title", typeof(string));
MainDataBase.ListBooks.Columns.Add("Author", typeof(string));
MainDataBase.ListBooks.Columns.Add("Gender", typeof(string));
MainDataBase.ListBooks.Columns.Add("Price", typeof(decimal));
MainDataBase.ListBooks.Columns.Add("Quantity", typeof(int));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
