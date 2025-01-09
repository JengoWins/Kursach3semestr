using ArchiveFiles.Models.FileWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcessFiles.Classes;
using Newtonsoft.Json;
using ArchiveFiles.DataBaseClasses;
using ArchiveFiles.Models.Template;
using ArchiveFiles.RabbitMQ;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ArchiveFiles.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ArchiveFilesController : ControllerBase
{
    private readonly ApplicationContext context;
    IWebHostEnvironment appEnvironment;

    public ArchiveFilesController(ApplicationContext context, IWebHostEnvironment appEnvironment) {
        this.context = context;
        this.appEnvironment = appEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> GetReLoadController()
    {
        return Ok("So, I'm job in Controller");
    }
    /// <summary>
    /// Запрос на вывод списка архивов
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetListArchives()
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("GetArchivePostList");
            List<ArchiveInfoModel?> archiveInfoModels = context.Archive_Info.ToList();
            List<FileInfoModel?> infoModels = context.File_Info.ToList();
            List<DataArchiveTemplate> templates = new List<DataArchiveTemplate>();
            for (int i = 0; i < archiveInfoModels.Count; i++)
            {
                DataArchiveTemplate temp = new DataArchiveTemplate
                {
                    name = infoModels[i].name,
                    path = infoModels[i].path,
                    date = infoModels[i].date,
                    dowloaded = archiveInfoModels[i].download,
                    size = archiveInfoModels[i].size
                };
                templates.Add(temp);    
            }
            return Ok(templates);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на вывод архива
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPostArchive(string nameFile)
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("GetArchivesPost");
            List<DataArchiveTemplate> templates = new List<DataArchiveTemplate>();
            foreach (Guid guid in listGuid)
            {
                FileInfoModel infoModel = context.File_Info.FirstOrDefault(a => a.name == nameFile);
                ArchiveModel archive = context.Archive.FirstOrDefault(a => a.id_Post == guid && a.id_FileInfo==infoModel.id);
                ArchiveInfoModel? archiveInfoModels = context.Archive_Info.FirstOrDefault(a=>a.id == archive.id_ArchiveInfo);

                DataArchiveTemplate temp = new DataArchiveTemplate
                {
                    name = infoModel.name,
                    path = infoModel.path,
                    date = infoModel.date,
                    dowloaded = archiveInfoModels.download,
                    size = archiveInfoModels.size
                };
                templates.Add(temp);
            }
            return Ok(templates);
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
    /// <summary>
    /// Запрос на создание архива
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ArchiveLoad(ArchiveTemplate FilePack)
    {
        try
        {
            if (FilePack != null)
            {
                // путь к папке Files
                string path = "../Site/Files/Archives/" + FilePack.File.FileName;
                ProcessFile MainFiles = new ProcessFile();
                DataArchiveTemplate datas = await MainFiles.LoadArchive(FilePack, appEnvironment, path);
                RabbitMq rabbit = new RabbitMq();
                List<Guid> listGuid = await rabbit.ReceiveMessage("CreateArchivesPost");
                foreach (Guid guid in listGuid)
                {
                    FileInfoModel infoModel = new FileInfoModel { 
                        name = datas.name,
                        path = datas.path,
                        date = datas.date,
                    };
                    ArchiveInfoModel? archiveInfoModels = new ArchiveInfoModel
                    {
                        download = 0,
                        size = datas.size,
                    };

                    ArchiveModel archive = new ArchiveModel()
                    {
                        id_ArchiveInfo = archiveInfoModels.id,
                        id_FileInfo = infoModel.id,
                        id_Post = guid
                    };
                    context.File_Info.AddRange(infoModel);
                    context.SaveChanges();
                    context.Archive_Info.AddRange(archiveInfoModels);
                    context.SaveChanges();
                    context.Archive.AddRange(archive);
                    context.SaveChanges();
                }
                return Ok("Запись архива завершен");
            }
            else
            {
                return BadRequest("Ааа ну, я не создал файл");
            }
            
        }
        catch (Exception ex)
        {
            var json = JsonConvert.SerializeObject(ex)!;
            return BadRequest(json);
        }
    }
    /// <summary>
    /// Запрос на удаление архива
    /// </summary>
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteArchive(string nameFile)
    {
        try
        {
            RabbitMq rabbit = new RabbitMq();
            List<Guid> listGuid = await rabbit.ReceiveMessage("DeleteArchivesPost");
            foreach (Guid guid in listGuid)
            {
                FileInfoModel infoModel = context.File_Info.FirstOrDefault(a => a.name == nameFile);
                ArchiveModel archive = context.Archive.FirstOrDefault(a => a.id_Post == guid && a.id_FileInfo==infoModel.id);
                ArchiveInfoModel? archiveInfoModels = context.Archive_Info.FirstOrDefault(a => a.id == archive.id_ArchiveInfo);
                context.Archive.Remove(archive);
                context.File_Info.Remove(infoModel);
                context.Archive_Info.Remove(archiveInfoModels);
                context.SaveChanges();
            }
            return Ok("Удаление завершено");
        }
        catch (Exception ex)
        {
            return BadRequest("Выпало исключение: " + ex);
        }
    }
}
