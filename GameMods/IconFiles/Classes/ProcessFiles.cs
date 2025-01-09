using IconFiles.Models.FileWork;
using IconFiles.Models.Template;

namespace IconFiles.Classes;

public class ProcessFile
{
    public async Task<FileInfoModel> LoadAvatar(IconPostTemplate FilePack, IWebHostEnvironment appEnvironment, string path) {
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
