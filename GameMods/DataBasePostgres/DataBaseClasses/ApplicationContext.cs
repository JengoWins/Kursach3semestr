using DataBasePostgres.Models.AutoReg;
using DataBasePostgres.Models.FileWork;
using DataBasePostgres.Models.LoadModsModel;
using Microsoft.EntityFrameworkCore;

namespace DataBasePostgres.DataBaseClasses;

public class ApplicationContext : DbContext
{
    public DbSet<AutorizationModel> Account { get; set; } = null!;
    public DbSet<RegistrationModel> Account_Info { get; set; } = null!;
    public DbSet<CategoryGame> CategoryGames { get; set; } = null!;
    public DbSet<FilesAvatarModel> AccountAvatar { get; set; } = null!;
    public DbSet<CreateModelPost> PostMods { get; set; } = null!;
    public DbSet<CategoryVersionFile> DevelopmentProcess { get; set; } = null!;
    public DbSet<FileArchiveModel> ArchiveFile { get; set; } = null!;
    public DbSet<FilesImagesModel> PostImages { get; set; } = null!;
    public DbSet<FilesIconModel> IconPost { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=GameServiceMods;Username=postgres;Password=SaraParker206");
    }
}