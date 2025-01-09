namespace Posts.Models.LoadModsModel;


//Уже развернутый пост (Внешние атрибуты)

public class PostInfoModel
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string miniDescript { get; set; }
    public string Descript { get; set; }
    public DateTime Date{ get; set; }
    public PostInfoModel()  
    {
        id = Guid.NewGuid();
    }
}
