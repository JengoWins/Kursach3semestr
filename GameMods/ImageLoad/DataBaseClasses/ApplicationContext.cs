using ArchiveFiles.Models.FileWork;
using Microsoft.EntityFrameworkCore;

namespace ArchiveFiles.DataBaseClasses;

public class ApplicationContext : DbContext
{
    public DbSet<ArchiveModel> Archive { get; set; } = null!;
    public DbSet<ArchiveInfoModel> Archive_Info { get; set; } = null!;

    public DbSet<FileInfoModel> File_Info { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=GameMods_File;Username=postgres;Password=SaraParker206");
    }
}