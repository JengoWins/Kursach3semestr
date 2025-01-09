using ImageFiles.Models.FileWork;
using Microsoft.EntityFrameworkCore;

namespace ImageFiles.DataBaseClasses;

public class ApplicationContext : DbContext
{
    public DbSet<FileInfoModel> File_Info { get; set; } = null!;
    public DbSet<ImagesModel> Images { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=GameMods_Image;Username=postgres;Password=SaraParker206");
    }
}