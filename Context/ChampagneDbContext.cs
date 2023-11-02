using System;
using System.Collections.Generic;
using ChampagneApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChampagneApi.Context;

public partial class ChampagneDbContext : IdentityDbContext<ApplicationUser> 
{
    public ChampagneDbContext()
    {
    }

    public ChampagneDbContext(DbContextOptions<ChampagneDbContext> options)
        : base(options)
    {
    }


    public DbSet<Size> Sizes { get; set; }
    public DbSet<GrapeVariety> GrapeVarieties { get; set; }
    public DbSet<Champagne> Champagnes { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Composition> Compositions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
