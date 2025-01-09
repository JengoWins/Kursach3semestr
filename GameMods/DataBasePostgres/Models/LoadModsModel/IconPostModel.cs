namespace DataBasePostgres.Models.LoadModsModel;

//краткое представление постов, ввиде списка 
public class IconPostsModel
{
    public string Category { get; set; }
    public string namePost { get; set; }
    public string miniDescript { get; set; }
    public DateTime datePost { get; set; }
    public string PathImage { get; set; }
}
