namespace DataBasePostgres.Models.LoadModsModel;

public class CategoryGameModelView
{
    public string category { get; set; }
}

public class CategoryGame
{
    public Guid id { get; set; }
    public string category { get; set; }

    public CategoryGame()
    {
        id = Guid.NewGuid();
    }
}
