using DataBasePostgres.DataBaseClasses;
using DataBasePostgres.Models.FileWork;
using System.IO;

namespace ProcessFiles.Classes;

public class ProcessFile
{
    public async void Load(FilesImageView FilePack, IWebHostEnvironment appEnvironment, ApplicationContext context, string path) {
        // сохраняем файл в папку Files в каталоге wwwroot
        using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
        {
            await FilePack.uploadedFile.CopyToAsync(fileStream);
        }
        FilesImageModel file = new FilesImageModel { name = FilePack.uploadedFile.FileName, path = path, user_id = FilePack.user_id };
        context.Files_Image.Add(file);
        context.SaveChanges();
    }
}
