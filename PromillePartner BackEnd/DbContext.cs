using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd
{
    public class VoresDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //connect to database on local computer
            options.UseSqlServer(@"Data Source=mssql4.unoeuro.com;Initial Catalog=tensormind_dk_db_pp; User Id=tensormind_dk; Password=hFkgbaAHeEmRxyGzdD64; TrustServerCertificate=true");
        }

        //case sensitiv i koden, den smider ikke hvis den ikke kan finde tabellen
        public DbSet<Person> Persons { get; set; }
        public DbSet<PiReading> PiReadings { get; set; }
    }
}
