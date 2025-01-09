using AvatarFiles.Models.FileWork;
using AvatarFiles.Models.Template;

namespace AvatarFiles.Classes;

public class ProcessFile
{
    public async Task<FileInfoModel> LoadAvatar(AvatarTemplate FilePack, IWebHostEnvironment appEnvironment, string path) {
        // сохраняем файл в папку Files в каталоге wwwroot
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await FilePack.File.CopyToAsync(fileStream);
        }
        FileInfoModel file = new FileInfoModel
        {
            name = FilePack.File.FileName,
            path = path,
            date = DateTime.Now,
        };
        return file;
    }
}
