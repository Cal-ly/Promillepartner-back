using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Data;

public class VoresDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<VoresDbContext>()
            .Build();

        // Get connection string from configuration
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Connect to database
        options.UseSqlServer(connectionString);
    }

    //case sensitiv i koden, den smider ikke hvis den ikke kan finde tabellen
    public DbSet<Person> Persons { get; set; }
    public DbSet<PiReading> PiReadings { get; set; }

    public VoresDbContext(DbContextOptions<VoresDbContext> options) : base(options)
    {
    }

    public VoresDbContext()
    {
    }
}
