﻿using EnergencyService.Models.AutoReg;
using Microsoft.EntityFrameworkCore;

namespace EnergencyService.DataBaseClasses;

public class ApplicationContext : DbContext
{
    public DbSet<AutorizationModel> Account { get; set; } = null!;
    public DbSet<RegistrationModel> Account_Info { get; set; } = null!;

    public DbSet<UserModel> User { get; set; } = null!;

    public ApplicationContext() { }
    public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=GameMods_User;Username=postgres;Password=SaraParker206");
    }
}