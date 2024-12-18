using DataBasePostgres.DataBaseClasses;
using DataBasePostgres.Models.FileWork;
using Microsoft.AspNetCore.Mvc;
using ProcessFiles.Classes;

namespace LoadFiles.Controllers
{
    public class ArchiveFilesController : Controller
    {
        private ApplicationContext context;
        IWebHostEnvironment appEnvironment;

        private ArchiveFilesController(ApplicationContext context, IWebHostEnvironment appEnvironment) {
            this.context = context;
            this.appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetReLoadController()
        {
            return Ok("So, I'm job in Controller");
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveLoad(FilesImageView FilePack)
        {
            try
            {
                if (FilePack != null)
                {
                    // путь к папке Files
                    string path = "/Files/Archives/" + FilePack.uploadedFile.FileName;
                    ProcessFile MainFiles = new ProcessFile();
                    MainFiles.Load(FilePack, appEnvironment, context, path);
                }

                return Ok("Mission Complete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ImagePostLoad(string namePost, FilesImageView FilePack)
        {
            try
            {
                if (FilePack != null)
                {
                    // путь к папке Files
                    string path = "/Files/ImagePost/" + namePost + "/" + FilePack.uploadedFile.FileName;
                    ProcessFile MainFiles = new ProcessFile();
                    MainFiles.Load(FilePack, appEnvironment, context, path);
                }

                return Ok("Mission Complete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> FilesPostLoad(string namePost, FilesImageView FilePack)
        {
            try
            {
                if (FilePack != null)
                {
                    // путь к папке Files
                    string path = "/Files/FilePost/" + namePost + "/" + FilePack.uploadedFile.FileName;
                    ProcessFile MainFiles = new ProcessFile();
                    MainFiles.Load(FilePack, appEnvironment, context, path);
                }

                return Ok("Mission Complete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
