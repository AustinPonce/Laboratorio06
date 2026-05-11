using Productos.DA;
using Productos.BL;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var cultureInfo = new CultureInfo("es-CR");

CultureInfo.DefaultThreadCurrentCulture =
    cultureInfo;

CultureInfo.DefaultThreadCurrentUICulture =
    cultureInfo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure EF Core and repository
var conn =
    builder.Configuration.GetConnectionString(
        "DefaultConnection");

if (string.IsNullOrEmpty(conn))
{
    conn = "Data Source=productos.db";
}

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlite(conn));

builder.Services.AddScoped<IProductoRepository,
    ProductoRepository>();

builder.Services.AddScoped<IProductoService,
    ProductoService>();

var app = builder.Build();

// Ensure database and tables are created
using (var scope = app.Services.CreateScope())
{
    var db =
        scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Productos}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();