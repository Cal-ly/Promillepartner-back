﻿using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Data;

public class VoresDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<PiReading> PiReadings { get; set; }

    public VoresDbContext(DbContextOptions<VoresDbContext> options) : base(options)
    {
    }

    public VoresDbContext()
    {
    }
}