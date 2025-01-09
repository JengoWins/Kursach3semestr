namespace IconFiles.Models.FileWork;


public class FileInfoModel
{
    public Guid Id { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public DateTime date { get; set; }

    public FileInfoModel()
    {
        Id = Guid.NewGuid();
    }
}
