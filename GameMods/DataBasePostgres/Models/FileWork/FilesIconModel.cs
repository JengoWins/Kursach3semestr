namespace DataBasePostgres.Models.FileWork;

public class FilesIconView
{
    public string namePost { get; set; }
    public IFormFile file { get; set; }

}

public class FilesIconModel
{
    public Guid id { get; set; }
    public string Path { get; set; }
    public FilesIconModel()
    {
        id = Guid.NewGuid();
    }
}
