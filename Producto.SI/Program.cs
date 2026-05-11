using Microsoft.EntityFrameworkCore;
using Productos.BL;
using Productos.DA;
using System.Globalization;

var cultureInfo = new CultureInfo("es-CR");

CultureInfo.DefaultThreadCurrentCulture =
    cultureInfo;

CultureInfo.DefaultThreadCurrentUICulture =
    cultureInfo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Connection string
string connectionString =
    "Data Source=productos.db";

// DbContext
builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseSqlite(connectionString));

// Dependency Injection
builder.Services.AddScoped<IProductoRepository,
    ProductoRepository>();

builder.Services.AddScoped<IProductoService,
    ProductoService>();

var app = builder.Build();

// Create database automatically
using (var scope = app.Services.CreateScope())
{
    AppDbContext dbContext =
        scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();