namespace IconFiles.Models.FileWork;

public class Image_IconPostModel
{
    public Guid Id { get; set; }
    public Guid id_FileInfo { get; set; }
    public Guid id_Post { get; set; }

    public Image_IconPostModel() {
        Id = Guid.NewGuid();
    }
}
