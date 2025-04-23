using System.IO;
using EfcRepositories;
using inter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
        policy.WithOrigins("http://localhost:5000") // Update with your Blazor frontend URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// Ensure database directory exists
var databasePath = "/app/data/database.sqlite";
var databaseDir = Path.GetDirectoryName(databasePath);

if (!Directory.Exists(databaseDir))
{
    Directory.CreateDirectory(databaseDir);
}

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, EfcPostRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));

var app = builder.Build();




// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();  // Ensures database schema is created
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazor");

app.UseRouting();
app.MapControllers();


app.Run();