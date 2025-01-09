namespace ArchiveFiles.Models.FileWork;

public class ArchiveInfoModel
{
    public Guid id { get; set; }
    public long size { get; set; }
    public int download { get; set; }

    public ArchiveInfoModel() 
    {
        id = Guid.NewGuid();
    }
}
