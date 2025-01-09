using AvatarFiles.Models.FileWork;
using Microsoft.EntityFrameworkCore;

namespace AvatarFiles.DataBaseClasses;

public class ApplicationContext : DbContext
{
    public DbSet<FileInfoModel> File_Info { get; set; } = null!;
    public DbSet<Image_AvatarModel> Image_Avatar { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=GameMods_Avatar;Username=postgres;Password=SaraParker206");
    }
}