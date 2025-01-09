namespace Posts.Models.LoadModsModel;

public class CategoryGameModel
{
    public Guid id { get; set; }
    public string category { get; set; }

    public CategoryGameModel()
    {
        id = Guid.NewGuid();
    }
}
