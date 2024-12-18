using DataBasePostgres.DataBaseClasses;
using DataBasePostgres.Models.AutoReg;
using DataBasePostgres.Models.LoadModsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FilePost.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private ApplicationContext context;
        private CreateModelPost PostMod;
        private CategoryGame? CategGame;
        private AutorizationModel? User;

        public PostsController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReLoadController()
        {
            return Ok("So, I'm job in Controller");
        }
        //Get posts and description data
        [HttpGet]
        public async Task<IActionResult> GetListPosts()
        {
            List<CreateModelPost>? createModelPosts = context.PostMods.ToList();
            List<IconPostsModel>? icons = new List<IconPostsModel>();
            for (int i = 0; i < createModelPosts.Count; i++)
            {
                CategGame = context.CategoryGames.FirstOrDefault(a => a.id == createModelPosts[i].typeGame_id);
                IconPostsModel icon = new IconPostsModel
                {
                    namePost = createModelPosts[i].namePost,
                    Category = CategGame!.category,
                    datePost = createModelPosts[i].DatePost,
                    miniDescript = createModelPosts[i].miniDescript
                };
                icons.Add(icon);
            }
            CategGame = null;
            return Ok(icons);
        }
        [HttpGet]
        public async Task<IActionResult> GetPost(string namePost)
        {
            try
            {
                CreateModelPost? ModelPost = context.PostMods.FirstOrDefault(a => a.namePost == namePost);
                if (ModelPost != null)
                    return Ok(ModelPost);
                else
                    return BadRequest("Пустая модель данных");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListImagePost()
        {
            return Ok("So, I'm job in Controller");
        }

        [HttpGet]
        public async Task<IActionResult> GetListFilePost()
        {
            return Ok("So, I'm job in Controller");
        }
        //Create Posts in site and add files many types
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreateModelController data)
        {
            try
            {
                User = context.Account.FirstOrDefault(a => a.id == data.User_ID);
                CategGame = context.CategoryGames.FirstOrDefault(a => a.category == data.typeGame);

                PostMod = new CreateModelPost
                {
                    user_id = data.User_ID,
                    namePost = data.namePost,
                    typeGame_id = CategGame!.id,
                    Contact = User!.email,
                    miniDescript = data.miniDescriptionPost,
                    Descript = data.descriptionPost,
                    DatePost = DateTime.Now,
                };
                User = null;
                CategGame = null;
                context.PostMods.AddRange(PostMod);
                await context.SaveChangesAsync();
                return Ok(PostMod);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddFiles()
        {
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddImages()
        {
            return BadRequest();
        }
    }
}
