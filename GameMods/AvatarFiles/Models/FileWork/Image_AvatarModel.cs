namespace AvatarFiles.Models.FileWork;

public class Image_AvatarModel
{
    public Guid Id { get; set; }
    public Guid id_FileInfo { get; set; }
    public Guid id_User { get; set; }

    public Image_AvatarModel() {
        Id = Guid.NewGuid();
    }
}
