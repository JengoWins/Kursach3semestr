namespace DataBasePostgres.Models.LoadModsModel;

public class CreatePostView
{
        public string username { get; set; }
        public string namePost { get; set; }
        public string typeGame { get; set; }
        public string miniDescriptionPost { get; set; }
        public string descriptionPost { get; set; }
}

public class UpdatePostView
{
    public string? typeGame { get; set; }
    public string? miniDescriptionPost { get; set; }
    public string? descriptionPost { get; set; }
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
    public Guid? Icon_id { get; set; }

    public CreateModelPost()
    {
        id = Guid.NewGuid();
    }
}