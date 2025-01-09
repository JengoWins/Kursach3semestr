using IconFiles.Models.FileWork;
using Microsoft.EntityFrameworkCore;

namespace IconFiles.DataBaseClasses;

public class ApplicationContext : DbContext
{
    public DbSet<FileInfoModel> File_Info { get; set; } = null!;
    public DbSet<Image_IconPostModel> Image_IconPost { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=GameMods_Icon;Username=postgres;Password=SaraParker206");
    }
}