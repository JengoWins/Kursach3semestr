using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataBasePostgres.DataBaseClasses;
using DataBasePostgres.Models.AutoReg;
using Microsoft.EntityFrameworkCore;
using System.IO;
using DataBasePostgres.Models.FileWork;
using System;
using ProcessFiles.Classes;


namespace LoadFiles.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageFilesController : ControllerBase
    {
        private ApplicationContext context;
        IWebHostEnvironment appEnvironment;

        public ImageFilesController(ApplicationContext context, IWebHostEnvironment appEnvironment) {
            this.context = context;
            this.appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetReLoadController()
        {
            return Ok("So, I'm job in Controller");
        }

        [HttpPost]
        public async Task<IActionResult> PhotoLoad(FilesImageView FilePack)
        {
            try
            {
                if (FilePack != null)
                {
                    // путь к папке Files
                    string path = "/Files/ImageAvatar/" + FilePack.uploadedFile.FileName;
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
        public async Task<IActionResult> ImagePostLoad(string namePost,FilesImageView FilePack)
        {
            try
            {
                if (FilePack != null)
                {
                    // путь к папке Files
                    string path = "/Files/ImagePost/"+namePost+"/" + FilePack.uploadedFile.FileName;
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
