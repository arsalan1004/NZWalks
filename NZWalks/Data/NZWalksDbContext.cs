using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domains;

namespace NZWalks.Data;

// DbContext class is sed to communicate with the database, save changes and facilitate CRUD functionality in it.
// We inherit into our class the EF Core DbContext class which primarily does the connection and setup work for us 
public class NZWalksDbContext: DbContext
{
    protected readonly IConfiguration Configuration;

    public NZWalksDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("NZDb"));
    }
    
    // Now we register the tables which are going to be added into our database which will assist us during migrations
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}