using ent;
using Microsoft.EntityFrameworkCore;


namespace EfcRepositories;

public class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }
}