namespace DataBasePostgres.Models.LoadModsModel;

//краткое представление постов, ввиде списка 
public class IconPostsModel{
    public string Category { get; set; }
    public string namePost { get; set; }
    public string miniDescript { get; set; }
    public DateTime datePost { get; set; }

}

public class IconFilesModel
{
    public string name { get; set; }
    public string description { get; set; }
    public DateTime datePost { get; set; }
}


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
//Файл, который представляет статистику
public class ViewFileModel
{
    public Guid id { get; set; }
    public string nameFile { get; set; }
    public string description { get; set; }
    public string Category { get; set; }
    public string createUser { get; set; }
    public string sizefile { get; set; }
    public string DownloadUser { get; set; }
    public string PreviewFile { get; set; }
    public DateTime datePost { get; set; }

    public ViewFileModel()
    {
        id = Guid.NewGuid();
    }
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

//Формы для создания поста
public class CreateModelPost
{
    public Guid id { get; set; }
    public Guid user_id { get; set; }
    public string namePost { get; set; }
    public Guid typeGame_id { get; set; }
    public string Contact { get; set; }
    public string miniDescript { get; set; }
    public string Descript { get; set; }
    public DateTime DatePost { get; set; }

    public CreateModelPost(){
        id = Guid.NewGuid();
    }
}


public class CreateFileModel
{
    public string nameFile { get; set; }
    public string description { get; set; }
    public string Category { get; set; }
    public Guid user_id { get; set; }
    public string sizefile { get; set; }
    public string DownloadUser { get; set; }
    public string PreviewFile { get; set; }
    public DateTime datePost { get; set; }

}

