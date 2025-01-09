namespace ImageFiles.Models.FileWork;

public class ImagesModel
{
    public Guid Id { get; set; }
    public Guid id_FileInfo { get; set; }
    public Guid id_Post { get; set; }

    public ImagesModel() {
        Id = Guid.NewGuid();
    }
}
