namespace ArchiveFiles.Models.FileWork;


public class ArchiveModel
{
    public Guid id { get; set; }
    public Guid id_ArchiveInfo { get; set; }
    public Guid id_Post { get; set; }
    public Guid id_FileInfo { get; set; }

    public ArchiveModel(){
        id = Guid.NewGuid();
    }

}
