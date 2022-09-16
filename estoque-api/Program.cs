using System.IO.IsolatedStorage;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlTypes;

using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

using storage.Repository;
using storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var rewriteOption = new RewriteOptions();
    rewriteOption.AddRedirect("^$", "swagger");
    app.UseRewriter(rewriteOption);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

