using Microsoft.AspNetCore.Mvc;
using AvatarFiles.DataBaseClasses;
using AvatarFiles.Models.FileWork;
using AvatarFiles.Classes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AvatarFiles.Models.Template;
using AvatarFiles.RabbitMQ;
using System;
using Microsoft.AspNetCore.Authorization;


namespace AvatarFiles.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AvatarController : ControllerBase
{
    private ApplicationContext context;
    IWebHostEnvironment appEnvironment;
    public AvatarController(ApplicationContext context, IWebHostEnvironment appEnvironment)
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
    /// Запрос на вывод аватарки
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAvatar()
    {
        try
        {
            RabbitMq rabbit = new RabbitMq(); 
            List<Guid> listGuid = await rabbit.ReceiveMessage("GetAvatarAccount");
            List<FileInfoModel> fileInfoModel = new List<FileInfoModel>();
            foreach (Guid guid in listGuid)
            {
                Image_AvatarModel avatarModel = context.Image_Avatar.FirstOrDefault(a=>a.id_User==guid);
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.Id == avatarModel.id_FileInfo);
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
    /// Запрос на вывод аватарок
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListAvatar()
    {
        try
        {
            List<Image_AvatarModel> avatarModel = context.Image_Avatar.ToList();
            List<FileInfoModel> fileInfoModel = new List<FileInfoModel>();
            foreach (Image_AvatarModel item in avatarModel)
            {
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a=>a.Id == item.id_FileInfo);
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
    /// Запрос на создание аватарки
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAvatar(AvatarTemplate FilePack)
    {
        try
        {
            if (FilePack != null)
            {
                RabbitMq rabbit = new RabbitMq();
                List<Guid> listGuid = await rabbit.ReceiveMessage("CreateAvatarAccount");
                // путь к папке Files
                string path = "../Site/Files/ImageAvatar/" + FilePack.File.FileName;
                ProcessFile MainFiles = new ProcessFile();
                FileInfoModel model_info = await MainFiles.LoadAvatar(FilePack,appEnvironment,path);
                context.File_Info.AddRange(model_info);
                context.SaveChanges();
                foreach (Guid guid in listGuid)
                {
                    //logger.LogInformation("KeyUser: " + guid);
                    Image_AvatarModel model_avatar= new Image_AvatarModel()
                    {
                        id_FileInfo = model_info.Id,
                        id_User = guid
                    };
                    context.Image_Avatar.AddRange(model_avatar);
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
    /// Запрос на изменение аватарки
    /// </summary>
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAvatar(AvatarTemplate FilePack)
    {
        try
        {
            if (FilePack != null)
            {

                RabbitMq rabbit = new RabbitMq();
                List<Guid> listGuid = await rabbit.ReceiveMessage("UpdateAvatarAccount");
                List<FileInfoModel> fileInfoModel = new List<FileInfoModel>();
                foreach (Guid guid in listGuid)
                {
                    Image_AvatarModel avatarModel = context.Image_Avatar.FirstOrDefault(a => a.id_User == guid);
                    FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.Id == avatarModel.id_FileInfo);
                    fileInfoModel.Add(fileInfoModel1);
                }

                foreach (FileInfoModel item in fileInfoModel)
                {
                    // путь к папке Files
                    string path = "../Site/Files/ImageAvatar/" + FilePack.File.FileName;
                    ProcessFile MainFiles = new ProcessFile();
                    FileInfoModel model_info = await MainFiles.LoadAvatar(FilePack, appEnvironment, path);
                    item.path = model_info.path;
                    item.name = model_info.name;
                    item.date = model_info.date;
                    //ArchiveInfoModel files = await MainFiles.LoadAvatar(FilePack, appEnvironment, path);
                    context.File_Info.UpdateRange(item);
                    context.SaveChanges();
                }
                return Ok("Изменена картинка");
            }
            else
            {
                //return BadRequest("Ааа ну, я не создал иконку для поста");
                RabbitMq rabbit = new RabbitMq();
                List<Guid> listGuid = await rabbit.ReceiveMessage("UpdateImageAccount"); //Получает поток, дабы его удалить.
                return Ok("Отмена обновления картинки");
            }

        }
        catch (Exception ex)
        {
            var json = JsonConvert.SerializeObject(ex)!;
            return BadRequest("Выпало исключение: " + json);
        }
    }
    /// <summary>
    /// Запрос на удаление аватарки
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteAvatar()
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("DeleteAvatarAccount");

            foreach (Guid guid in listGuid)
            {
                Image_AvatarModel avatarModel = context.Image_Avatar.FirstOrDefault(a => a.id_User == guid);
                FileInfoModel fileInfoModel1 = context.File_Info.FirstOrDefault(a => a.Id == avatarModel.id_FileInfo);
                context.Image_Avatar.Remove(avatarModel);
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