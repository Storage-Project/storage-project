using System.IO.IsolatedStorage;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlTypes;

using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

using storage.Repository;
using storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Controllers
builder.Services.AddControllers();

// Add Database context
builder.Services.AddDbContext<AppDbContext>();

// Add repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add Swagger Dependencies
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build Application
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Auto migration based on Database context
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger Use
    app.UseSwagger();
    app.UseSwaggerUI();

    // Redirects to Swagger
    var rewriteOption = new RewriteOptions();
    rewriteOption.AddRedirect("^$", "swagger");
    app.UseRewriter(rewriteOption);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
