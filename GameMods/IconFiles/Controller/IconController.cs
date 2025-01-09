using Microsoft.AspNetCore.Mvc;
using IconFiles.RabbitMQ;
using IconFiles.DataBaseClasses;
using Microsoft.EntityFrameworkCore;
using IconFiles.Models.FileWork;
using IconFiles.Models.Template;
using IconFiles.Classes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace IconFiles.Controller;

[Route("api/[controller]/[action]")]
[ApiController]
public class IconController : ControllerBase
{
    private ApplicationContext context;
    IWebHostEnvironment appEnvironment;

    public IconController(ApplicationContext context, IWebHostEnvironment appEnvironment)
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
    /// Запрос на вывод иконки
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetIcon()
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("GetIconPost");
            List<FileInfoModel> fileInfoModel = new List<FileInfoModel>();
            foreach (Guid guid in listGuid)
            {
                Image_IconPostModel avatarModel = context.Image_IconPost.FirstOrDefault(a => a.id_Post == guid);
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.Id == avatarModel.id_FileInfo);
                fileInfoModel.Add(fileInfoModel1);
            }

            return Ok(fileInfoModel);
        }
        catch (Exception ex)
        {
            var json = JsonConvert.SerializeObject(ex)!;
            return BadRequest("Выпало исключение: " + json);
        }
    }
    /// <summary>
    /// Запрос на вывод всех иконок постов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListIcon()
    {
        try
        {
            //RabbitMq rabbit = new RabbitMq();
            //List<Guid> listGuid = await rabbit.ReceiveMessage("GetPostName");
            List<Image_IconPostModel> avatarModel = context.Image_IconPost.ToList();
            List<FileInfoModel> fileInfoModel = new List<FileInfoModel>();
            foreach (Image_IconPostModel item in avatarModel)
            {
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.Id == item.id_FileInfo);
                fileInfoModel.Add(fileInfoModel1);
            }

            return Ok(fileInfoModel);
        }
        catch (Exception ex)
        {
            var json = JsonConvert.SerializeObject(ex)!;
            return BadRequest("Выпало исключение: " + json);
        }
    }
    /// <summary>
    /// Запрос на создание иконки
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateIcon(IconPostTemplate FilePack)
    {
        try
        {
            if (FilePack != null)
            {
                RabbitMq rabbit = new RabbitMq();
                List<Guid> listGuid = await rabbit.ReceiveMessage("CreateIconPost");
                // путь к папке Files
                string path = "../Site/Files/IconImage/" + FilePack.File.FileName;
                ProcessFile MainFiles = new ProcessFile();
                FileInfoModel model_info = await MainFiles.LoadAvatar(FilePack, appEnvironment, path);
                context.File_Info.AddRange(model_info);
                context.SaveChanges();
                foreach (Guid guid in listGuid)
                {
                    //logger.LogInformation("KeyUser: " + guid);
                    Image_IconPostModel model_avatar = new Image_IconPostModel()
                    {
                        id_FileInfo = model_info.Id,
                        id_Post = guid
                    };
                    context.Image_IconPost.AddRange(model_avatar);
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
    /// Запрос на удаление иконки
    /// </summary>
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteIcon()
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("DeleteIconPost");

            foreach (Guid guid in listGuid)
            {
                Image_IconPostModel avatarModel = context.Image_IconPost.FirstOrDefault(a => a.id_Post == guid);
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.Id == avatarModel.id_FileInfo);
                context.Image_IconPost.Remove(avatarModel);
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
