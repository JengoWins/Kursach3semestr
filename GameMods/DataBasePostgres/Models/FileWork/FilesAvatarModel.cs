namespace DataBasePostgres.Models.FileWork;

public class FilesAvatarView
{
    public Guid user_id { get; set; }
    public IFormFile uploadedFile { get; set; }
}

public class FilesAvatarModel
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public Guid user_id { get; set; }

    public FilesAvatarModel() 
    {
        id = Guid.NewGuid();
    }
}
