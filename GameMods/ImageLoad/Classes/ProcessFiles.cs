using ArchiveFiles.Models.FileWork;
using ArchiveFiles.Models.Template;

namespace ProcessFiles.Classes;

public class ProcessFile
{

    public async Task<DataArchiveTemplate> LoadArchive(ArchiveTemplate FilePack, IWebHostEnvironment appEnvironment, string pathArchive)
    {
        // сохраняем файл в папку Files в каталоге wwwroot
        using (var fileStream = new FileStream(pathArchive, FileMode.Create))
        {
            await FilePack.File.CopyToAsync(fileStream);
        }

        DataArchiveTemplate file = new DataArchiveTemplate
        {
           name = FilePack.File.FileName,
           path = pathArchive,
           size = FilePack.File.Length,
           dowloaded = 0,
           date = DateTime.Now,
        };
        return file;
    }
}
