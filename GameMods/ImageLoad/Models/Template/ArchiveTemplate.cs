namespace ArchiveFiles.Models.Template;

public class ArchiveTemplate
{
    public IFormFile File { get; set; }
}

public class DataArchiveTemplate
{
    public string name { get; set; }
    public string path { get; set; }
    public DateTime date { get; set; }
    public long size { get; set; }
    public int dowloaded { get; set; }

}