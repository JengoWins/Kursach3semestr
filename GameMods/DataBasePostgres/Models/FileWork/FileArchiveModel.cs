namespace DataBasePostgres.Models.FileWork;

public class FileArchiveView
{
    public string username { get; set; }
    public string description { get; set; }
    public string development_process { get; set; }
    public IFormFile ArchiveFile { get; set; }
    public IFormFile PreviewFile { get; set; }
}

public class FileArchiveModel
{
    public Guid id { get; set; }
    public string nameFile { get; set; }
    public string description { get; set; }
    public Guid development_process { get; set; }
    public Guid user_id { get; set; }
    public long sizefile { get; set; }
    public int DownloadUser { get; set; }
    public string ArchiveFilePath { get; set; }
    public string PreviewFilePath { get; set; }
    public DateTime datePost { get; set; }
    public Guid post_id { get; set; }

    public FileArchiveModel(){
        id = Guid.NewGuid();
    }

}
