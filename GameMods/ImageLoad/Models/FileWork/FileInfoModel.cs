namespace ArchiveFiles.Models.FileWork;


public class FileInfoModel
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public DateTime date { get; set; }

    public FileInfoModel()
    {
        id = Guid.NewGuid();
    }
}
