using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Data;

/// <summary>
/// DbContext it retrieves connectionstring from secret json
/// </summary>
public class VoresDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<PiReading> PiReadings { get; set; }
    public DbSet<DrinkPlan> DrinkPlan { get; set; }

    public VoresDbContext(DbContextOptions<VoresDbContext> options) : base(options)
    {
    }

    public VoresDbContext()
    {
    }
}
