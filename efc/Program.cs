using EfcRepositories;
using Microsoft.AspNetCore.Builder;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);



// Configure SQLite with the correct database path
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=/app/data/database.sqlite"));
builder.Services.AddAntiforgery(options => options.SuppressXFrameOptionsHeader = true);


var app = builder.Build();

// Ensure database and migrations are applied
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();  // Applies any pending migrations
}

app.Run();