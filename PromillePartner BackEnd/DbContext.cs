using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd
{
    public class VoresDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //connect to database on local computer
            options.UseSqlServer(@"Data Source=localhost;Initial Catalog=TestDatabase2; Integrated Security=True; Connect Timeout=30; Encrypt=False");
        }

        //case sensitiv i koden, den smider ikke hvis den ikke kan finde tabellen
        public DbSet<Person> Persons { get; set; }
        public DbSet<PiReading> PiReadings { get; set; }
    }
}
