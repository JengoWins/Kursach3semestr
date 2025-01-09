namespace Posts.Models.LoadModsModel;

public class DevelopmentProcessModel
{
    public Guid id { get; set; }
    public string category { get; set; }

    public DevelopmentProcessModel()
    {
        id = Guid.NewGuid();
    }
}
