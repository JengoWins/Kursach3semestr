namespace DataBasePostgres.Models.LoadModsModel;


//Уже развернутый пост (Внешние атрибуты)

public class ProfilePostModel
{
    public Guid id { get; set; }
    public string nameUser { get; set; }
    public string namePost { get; set; }
    public string typeGame { get; set; }
    public string Contact { get; set; }
    public ProfilePostModel()
    {
        id = Guid.NewGuid();
    }
}

public class CategoryVersionFile
{
    public Guid id { get; set; }
    public string category { get; set; }
}

