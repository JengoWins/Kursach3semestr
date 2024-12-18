namespace DataBasePostgres.Models.FileWork;

public class FilesImageView
{
    public Guid user_id { get; set; }
    public IFormFile uploadedFile { get; set; }
}

public class FilesImageModel
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public Guid user_id { get; set; }

    public FilesImageModel() 
    {
        id = Guid.NewGuid();
    }
}
