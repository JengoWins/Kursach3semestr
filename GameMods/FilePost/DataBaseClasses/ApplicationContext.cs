using Posts.Models.LoadModsModel;
using Microsoft.EntityFrameworkCore;

namespace Posts.DataBaseClasses;

public class ApplicationContext : DbContext
{
    public DbSet<CategoryGameModel> CategoryGame { get; set; } = null!;
    public DbSet<DevelopmentProcessModel> DevelopmentProcess { get; set; } = null!;
    public DbSet<PostInfoModel> Post_Info { get; set; } = null!;
    public DbSet<PostModel> Post { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=GameMods_Post;Username=postgres;Password=SaraParker206");
    }
}