using Posts.DataBaseClasses;
using Posts.Models.LoadModsModel;
using Posts.Models.Template;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Posts.RabbitMQ;
using System;


namespace Post.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private ApplicationContext context;
        private readonly ILogger logger;

        public PostsController(ApplicationContext context, ILogger<PostsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        /// <summary>
        /// Подтверждение авторизации
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReLoadController()
        {
            return Ok("Access Token");
        }
        /// <summary>
        /// Список категории игр
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCaregoryGame()
        {
            try
            {
                List<CategoryGameModel>? categoryGames = context.CategoryGame.ToList();
                List<CategoryGameTemplate>? ListGames = new List<CategoryGameTemplate>();
                for (int i = 0; i < categoryGames.Count; i++)
                {
                    CategoryGameTemplate icon = new CategoryGameTemplate
                    {
                        category = categoryGames[i]!.category,
                    };
                    ListGames.Add(icon);
                }
                return Ok(ListGames);
            }
            catch (Exception ex)
            {
                return BadRequest("Исключение: " + ex);
            }
        }
        /// <summary>
        /// Создание категорий игр
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCaregoryGame(CategoryGameTemplate view)
        {
            try
            {
                CategoryGameModel catGame = new CategoryGameModel
                {
                    category = view.category,
                };
                context.CategoryGame.AddRange(catGame);
                await context.SaveChangesAsync();
                return Ok(catGame);
            }
            catch (Exception ex)
            {
                return BadRequest("Исключение: " + ex);
            }
        }
        /// <summary>
        /// Редактирование категории игр
        /// </summary>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateCaregoryGame(string oldCategory, string newCategory)
        {
            try
            {
                CategoryGameModel catGame = context.CategoryGame.FirstOrDefault(x => x.category == oldCategory);
                catGame.category = newCategory;
                await context.SaveChangesAsync();
                return Ok(catGame);
            }
            catch (Exception ex)
            {
                return BadRequest("Исключение: " + ex);
            }
        }
        /// <summary>
        /// Удаление категории игр
        /// </summary>
        ///<returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteCaregoryGame(string category)
        {
            try
            {
                CategoryGameModel catGame = context.CategoryGame.FirstOrDefault(x => x.category == category);
                context.CategoryGame.Remove(catGame);
                await context.SaveChangesAsync();
                return Ok(catGame);
            }
            catch (Exception ex)
            {
                return BadRequest("Исключение: " + ex);
            }
        }
        //-------------------
        /// <summary>
        /// Запрос на выдачу ключа поста для активации запроса вывода всех картинок
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetImagesListPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                RabbitMq rabbitMq = new RabbitMq();
                rabbitMq.SendMessage(Post.id, "GetImagesListPost");
                return Ok("Отправка в Реббит...");
            }
            catch (Exception ex)
            {
                return BadRequest("Выпало исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на выдачу ключа поста для активации запроса вывода картинки
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetImagesPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                RabbitMq rabbitMq = new RabbitMq();
                rabbitMq.SendMessage(Post.id, "GetImagesPost");
                return Ok("Отправка в Реббит...");
            }
            catch (Exception ex)
            {
                return BadRequest("Выпало исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на выдачу ключа поста для активации запроса создания картинки
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateImagesPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                RabbitMq rabbitMq = new RabbitMq();
                rabbitMq.SendMessage(Post.id, "CreateImagesPost");
                return Ok("Отправка в Реббит...");
            }
            catch (Exception ex)
            {
                return BadRequest("Выпало исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на выдачу ключа поста для активации запроса удаления картинки
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteImagesPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                RabbitMq rabbitMq = new RabbitMq();
                rabbitMq.SendMessage(Post.id, "DeleteImagesPost");
                return Ok("Отправка в Реббит...");
            }
            catch (Exception ex)
            {
                return BadRequest("Выпало исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на выдачу ключа поста для активации запроса вывода списка архивов
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetArchivesListPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                RabbitMq rabbitMq = new RabbitMq();
                rabbitMq.SendMessage(Post.id, "GetArchivesListPost");
                return Ok("Отправка в Реббит...");
            }
            catch (Exception ex)
            {
                return BadRequest("Выпало исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на выдачу ключа поста для активации запроса вывода архива
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetArchivesPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                RabbitMq rabbitMq = new RabbitMq();
                rabbitMq.SendMessage(Post.id, "GetArchivesPost");
                return Ok("Отправка в Реббит...");
            }
            catch (Exception ex)
            {
                return BadRequest("Выпало исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на выдачу ключа поста для активации запроса создания архива
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateArchivesPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                RabbitMq rabbitMq = new RabbitMq();
                rabbitMq.SendMessage(Post.id, "CreateArchivesPost");
                return Ok("Отправка в Реббит...");
            }
            catch (Exception ex)
            {
                return BadRequest("Выпало исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на выдачу ключа поста для активации запроса на удаление архива
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteArchivesPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                RabbitMq rabbitMq = new RabbitMq();
                rabbitMq.SendMessage(Post.id, "DeleteArchivesPost");
                return Ok("Отправка в Реббит...");
            }
            catch (Exception ex)
            {
                return BadRequest("Выпало исключение: " + ex);
            }
        }
        //--------------------
        /// <summary>
        /// Запрос на вывод данных конкретного поста
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель информации поста. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель самих постов. Надо ввести существующий");
                CategoryGameModel? catGame = context.CategoryGame.FirstOrDefault(a => a.id == Post.id_Game);
                if (catGame == null)
                    logger.LogWarning("Пустая модель категории игр. Надо ввести существующий");
                DevelopmentProcessModel? processGame = context.DevelopmentProcess.FirstOrDefault(a => a.id == Post.id_Progress);
                if (processGame == null)
                    logger.LogInformation("Пустая модель категории прогресса разработки. Надо ввести существующий");
               

                RabbitMq rabbit = new RabbitMq();
                rabbit.SendMessage(Post.id_User,"PostGetUser");
                rabbit.SendMessage(Post.id, "GetIconPost");

                FullPostTemplate templatePost = new FullPostTemplate
                {
                    namePost = info.name,
                    typeGame = catGame.category,
                    Process = processGame.category,
                    miniDescriptionPost = info.miniDescript,
                    descriptionPost = info.Descript,
                    date = info.Date
                };
                if (templatePost != null)
                    return Ok(templatePost);
                else
                    return BadRequest("Пустая модель данных");
            }
            catch (Exception ex)
            {
                return BadRequest("Исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на вывод данных списка постов
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetListPosts()
        {
            try
            {
                List<PostModel>? Posts = context.Post.ToList();
                List<FullPostTemplate> postTemp = new List<FullPostTemplate>();
                for (int i = 0; i < Posts.Count; i++)
                {
                    PostInfoModel info = context.Post_Info.FirstOrDefault(a => a.id == Posts[i].id_info);
                    CategoryGameModel? CategGame = context.CategoryGame.FirstOrDefault(a => a.id == Posts[i].id_Game);
                    DevelopmentProcessModel processGame = context.DevelopmentProcess.FirstOrDefault(a => a.id == Posts[i].id_Progress);

                    FullPostTemplate post = new FullPostTemplate
                    {
                        namePost = info.name,
                        typeGame = CategGame.category,
                        Process = processGame.category,
                        miniDescriptionPost = info.miniDescript,
                        descriptionPost = info.Descript,
                        date = info.Date
                    };
                    postTemp.Add(post);
                }
                return Ok(postTemp);
            }
            catch (Exception ex)
            {
                return BadRequest("Исключение: " + ex);
            }
        }
        /// <summary>
        /// Запрос на создание поста
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(PostTemplate data)
        {
            try
            {
                RabbitMq rabbit =  new RabbitMq();
                List<Guid> listGuid = await rabbit.ReceiveMessage("PostCreateUser");
                foreach (Guid idUser in listGuid)
                {
                    logger.LogInformation("User G: "+ idUser);
                    CategoryGameModel? CategGame = context.CategoryGame.FirstOrDefault(a => a.category == data.typeGame);
                    if (CategGame == null)
                        logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                    DevelopmentProcessModel progress = context.DevelopmentProcess.FirstOrDefault(a => a.category == data.Process);
                    if (CategGame == null)
                        logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");

                    PostInfoModel model_info = new PostInfoModel
                    {
                        name = data.namePost,
                        miniDescript = data.miniDescriptionPost,
                        Descript = data.descriptionPost,
                        Date = DateTime.Now,
                    };

                    context.Post_Info.AddRange(model_info);
                    await context.SaveChangesAsync();

                    PostModel model_post = new PostModel
                    {
                        id_Game = CategGame.id,
                        id_Progress = progress.id,
                        id_info = model_info.id,
                        id_User = idUser
                    };

                    context.Post.AddRange(model_post);
                    await context.SaveChangesAsync();
                    rabbit.SendMessage(model_post.id, "CreateIconPost");
                }
                return Ok("Пост Создан");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Запрос на изменение поста
        /// </summary>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePost(string OldNamePost, PostTemplate data)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == OldNamePost);
                if (info == null)
                    logger.LogWarning("Пустая модель информации постов. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogWarning("Пустая модель самих постов. Надо ввести существующий");
                CategoryGameModel? catGame = context.CategoryGame.FirstOrDefault(a => a.category == data.typeGame);
                if (catGame == null)
                    logger.LogWarning("Пустая модель категории игр. Надо ввести существующий");
                DevelopmentProcessModel? processGame = context.DevelopmentProcess.FirstOrDefault(a => a.category == data.Process);
                if (processGame == null)
                    logger.LogWarning("Пустая модель категории прогресса разработки. Надо ввести существующий");

                info.name = data.namePost;
                info.miniDescript = data.miniDescriptionPost;
                info.Descript = data.descriptionPost;

                context.Post_Info.Update(info);
                await context.SaveChangesAsync();

                Post.id_Game = catGame.id;
                Post.id_Progress = processGame.id;

                context.Post.Update(Post);
                await context.SaveChangesAsync();

                return Ok("Изменения завершены");
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(ex)!;
                return BadRequest("Выпало исключение: " + json);
            }
        }
        /// <summary>
        /// Запрос на удаление поста
        /// </summary>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePost(string namePost)
        {
            try
            {
                PostInfoModel? info = context.Post_Info.FirstOrDefault(a => a.name == namePost);
                if (info == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");
                PostModel? Post = context.Post.FirstOrDefault(a => a.id_info == info.id);
                if (Post == null)
                    logger.LogInformation("Пустая модель категории игр. Надо ввести существующий");


                RabbitMq rabbit = new RabbitMq();
                rabbit.SendMessage(Post.id, "DeleteIconPost");

                context.Post.Remove(Post);
                await context.SaveChangesAsync();
                context.Post_Info.Remove(info);
                await context.SaveChangesAsync();
                
                return Ok("Пост удален");
            }
            catch (Exception ex)
            {
                return BadRequest("Исключение: " + ex);
            }
        }
        
    }
}
