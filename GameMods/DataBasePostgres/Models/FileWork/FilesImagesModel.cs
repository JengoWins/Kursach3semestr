namespace DataBasePostgres.Models.FileWork;

public class FilesImagesView
{
    public Guid user_id { get; set; }
    public string namePost { get; set; }
    public IFormFile File { get; set; }
}


public class FilesImagesModel
{
    public Guid id { get; set; }
    public Guid user_id { get; set; }
    public string PathFile { get; set; }
    public DateTime datePost { get; set; }
    public Guid post_id { get; set; }
    public string nameFile { get; set; }

    public FilesImagesModel()
    {
        id = Guid.NewGuid();
    }
}
