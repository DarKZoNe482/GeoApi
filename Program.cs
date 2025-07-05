using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données SQLite avec support spatial
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"), 
    x => x.UseNetTopologySuite()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod(); // Inclut OPTIONS
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
