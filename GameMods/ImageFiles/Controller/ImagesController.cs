using Microsoft.AspNetCore.Mvc;
using ImageFiles.RabbitMQ;
using ImageFiles.DataBaseClasses;
using Microsoft.EntityFrameworkCore;
using ImageFiles.Models.FileWork;
using ImageFiles.Models.Template;
using ImageFiles.Classes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ImageFiles.Controller;

[Route("api/[controller]/[action]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private ApplicationContext context;
    IWebHostEnvironment appEnvironment;

    public ImagesController(ApplicationContext context, IWebHostEnvironment appEnvironment)
    {
        this.context = context;
        this.appEnvironment = appEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> TestController()
    {
        return Ok("So, I'm job in Controller");
    }
    /// <summary>
    /// Запрос на вывод картинки
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetImage(string nameFile)
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("GetImagesPost");
            List<FileInfoModel> fileInfoModel = new List<FileInfoModel>();
            foreach (Guid guid in listGuid)
            {
                ImagesModel avatarModel = context.Images.FirstOrDefault(a => a.id_Post == guid);
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.Id == avatarModel.id_FileInfo && a.name == nameFile);
                fileInfoModel.Add(fileInfoModel1);
            }

            return Ok(fileInfoModel);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на вывод картинок
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetListImage()
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("GetImagesPostList");
            List<ImagesModel> avatarModel = new List<ImagesModel>();
            foreach (Guid guid in listGuid)
            {
                avatarModel = context.Images.Where(a=>a.id_Post == guid).ToList();
            }
                
            List<FileInfoModel> fileInfoModel = new List<FileInfoModel>();

            foreach (ImagesModel item in avatarModel)
            {
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.Id == item.id_FileInfo);
                fileInfoModel.Add(fileInfoModel1);
            }

            return Ok(fileInfoModel);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на добавление картинки
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateImage(ImagesTemplate FilePack)
    {
        try
        {
            if (FilePack != null)
            {
                RabbitMq rabbit = new RabbitMq();
                List<Guid> listGuid = await rabbit.ReceiveMessage("CreateImagesPost");
                // путь к папке Files
                string path = "../Site/Files/Images/" + FilePack.File.FileName;
                ProcessFile MainFiles = new ProcessFile();
                FileInfoModel model_info = await MainFiles.LoadAvatar(FilePack, appEnvironment, path);
                context.File_Info.AddRange(model_info);
                context.SaveChanges();

                foreach (Guid guid in listGuid)
                {
                    //logger.LogInformation("KeyUser: " + guid);
                    ImagesModel model_avatar = new ImagesModel()
                    {
                        id_FileInfo = model_info.Id,
                        id_Post = guid
                    };
                    context.Images.AddRange(model_avatar);
                    context.SaveChanges();
                }
                return Ok("Картинка загрузилась");
            }
            else
            {
                return BadRequest("Ааа ну, я не создал аватарку");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на удаление картинки
    /// </summary>
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteImage(string nameFile)
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("DeleteImagesPost");

            foreach (Guid guid in listGuid)
            {
                
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.name == nameFile);
                ImagesModel avatarModel = context.Images.FirstOrDefault(a => a.id_Post == guid && a.id_FileInfo == fileInfoModel1.Id);
                context.Images.Remove(avatarModel);
                context.SaveChanges();
                context.File_Info.Remove(fileInfoModel1);
                context.SaveChanges();
            }

            return Ok("Аватар успешно удалена");
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
}
